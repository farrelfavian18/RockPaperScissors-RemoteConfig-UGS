using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using System;

public class BotDifficultyManager : MonoBehaviour
{
    [SerializeField] Bot bot;
    [SerializeField] int selectedDifficulty;
    [SerializeField] BotStats[] botDifficulties;

    [Header("Remote Config Parameters:")]
    [SerializeField] bool enableRemoteConfig = false;
    [SerializeField] string difficultyKey = "Difficulty";

    struct userAttribute { };
    struct appAttribute { };

    IEnumerator Start()
    {
        //tunggu bot selesai set up
        yield return new WaitUntil(() => bot.IsReady);

        // set stats default dari difficulty manager
        //sesuai selectedDifficulty dari inspector
        var newStats = botDifficulties[selectedDifficulty];
        bot.SetStats(newStats);

        // Ambil difficulty remote config kalau enabled
        if (enableRemoteConfig == false)
            yield break;

        // tapi tunggu dulu sampe Unity Service siap
        yield return new WaitUntil(
            () =>
                UnityServices.State == ServicesInitializationState.Initialized
                &&
                AuthenticationService.Instance.IsSignedIn
            );

        //Daftar dulu untuk event fetch completed
        RemoteConfigService.Instance.FetchCompleted += OnRemoteConfigFetched;

        //lalu fetch disini cukup sekali di awal permainan
        RemoteConfigService.Instance.FetchConfigs
        (
            new userAttribute(), new appAttribute()
        );

    }

    private void OnDestroy()
    {
        //jangan lupa untuk unregister event untuk menghindari memory leak
        RemoteConfigService.Instance.FetchCompleted -= OnRemoteConfigFetched;
    }

    //setiap kali data baru didapat (melalui fetch) fungsi ini akan dipanggil
    private void OnRemoteConfigFetched(ConfigResponse response)
    {
        if (RemoteConfigService.Instance.appConfig.HasKey(difficultyKey) == false)
        {
            Debug.LogWarning($"Difficulty Key: {difficultyKey} not found on remote config server");
            return;
        }
        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
            case ConfigOrigin.Cached:
                break;
            case ConfigOrigin.Remote:
                selectedDifficulty = RemoteConfigService.Instance.appConfig.GetInt(difficultyKey);
                selectedDifficulty = Mathf.Clamp(selectedDifficulty, 0, botDifficulties.Length - 1);
                var newStats = botDifficulties[selectedDifficulty];
                bot.SetStats(newStats, true);
                break;
        }
    }
}
