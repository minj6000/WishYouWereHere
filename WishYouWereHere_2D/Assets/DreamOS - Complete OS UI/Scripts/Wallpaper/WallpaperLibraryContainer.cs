﻿using UnityEngine;

namespace Michsky.DreamOS
{
    [DisallowMultipleComponent]
    [AddComponentMenu("DreamOS/Wallpaper/Wallpaper Library Container")]
    public class WallpaperLibraryContainer : MonoBehaviour
    {
        // Resources
        [SerializeField] private WallpaperManager wallpaperManager;

        void Start()
        {
            FetchItems();
        }

        public void FetchItems()
        {
            // Check for wallpaper manager
            if (wallpaperManager == null)
            {
                if (FindObjectsOfType(typeof(WallpaperManager)).Length > 0) { wallpaperManager = (WallpaperManager)FindObjectsOfType(typeof(WallpaperManager))[0]; }
                else { Debug.Log("<b>[Wallpaper Library Container]</b> Wallpaper Manager is missing.", this); return; }
            }

            // Delete each cached objects
            foreach (Transform child in transform) { Destroy(child.gameObject); }

            // Create a new wallpaper object for each item
            wallpaperManager.InitializeWallpapers(transform);
        }
    }
}