using PixelCrushers.DialogueSystem;
using System;
using UniRx;
using UnityEngine;

namespace WishYouWereHere3D
{
    public static class DialogueExtensions
    {
        public static IObservable<Transform> ConversationEndedAsObservable(this DialogueSystemController dialogueSystemController)
        {           
            return Observable.FromEvent<TransformDelegate, Transform>(
                                h => t => h(t),
                                h => dialogueSystemController.conversationEnded += h,
                                h => dialogueSystemController.conversationEnded -= h);
        }

        public static void StartConversationWithEndedAction(this DialogueSystemController dialogueSystemController, string title, Action<Transform> action)
        {
            DialogueManager.Instance.StartConversation(title);
            ConversationEndedAsObservable(dialogueSystemController).First().Subscribe(action);
        }
    }
}
