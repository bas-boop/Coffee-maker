using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Baz_geluk9.CoffeeMaker
{
    public sealed class MainMenu : MonoBehaviour
    {
        [SerializeField] private string[] sceneNames = {"Gameplay"};
        [SerializeField] private GameObject main;
        [SerializeField] private GameObject story;
        [SerializeField] private GameObject backButton;
        [SerializeField] private GameObject storyButton;

        /// <summary>
        /// Loads a scene.
        /// </summary>
        /// <param name="index">The number of the scene you want to load.</param>
        public void LoadSceneButton(int index) => SceneManager.LoadScene(sceneNames[index]);

        public void StoryButton()
        {
            main.SetActive(false);
            story.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(backButton);
        }

        public void BackButton()
        {
            story.SetActive(false);
            main.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(storyButton);
        }

        /// <summary>
        /// Quit the application, aka the game closes.
        /// </summary>
        public void QuitButton() => Application.Quit();
    }
}
