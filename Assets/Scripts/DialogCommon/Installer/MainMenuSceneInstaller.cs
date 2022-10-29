using System.Collections.Generic;
using System.IO;
using DialogCommon.Model;
using DialogCommon.Model.AnswerAction;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace DialogCommon.Installer
{
    public class MainMenuSceneInstaller : MonoInstaller<MainMenuSceneInstaller>
    {
        public override void InstallBindings()
        {
            var scenario = new ScenarioModel
            {
                StartSceneId = 0,
                Scenes = new List<DialogSceneModel>
                {
                    new DialogSceneModel
                    {
                        Id = 0,
                        MainText = "Some text scene 0",
                        Answers = new List<AnswerModel>
                        {
                            new AnswerModel
                            {
                                AnswerActionType = AnswerActionType.TransitionToDialogScene,
                                AnswerActionModel = new TransitionAnswerActionModel
                                {
                                    ToDialogSceneId = 1,
                                    MainText = "Answer to 1"
                                }
                            },
                            new AnswerModel
                            {
                                AnswerActionType = AnswerActionType.TransitionToDialogScene,
                                AnswerActionModel = new TransitionAnswerActionModel
                                {
                                    ToDialogSceneId = 2,
                                    MainText = "Answer to 2"
                                }
                            },
                        }
                    },
                    new DialogSceneModel
                    {
                        Id = 1,
                        MainText = "Some text scene 1",
                        Answers = new List<AnswerModel>
                        {
                            new AnswerModel
                            {
                                AnswerActionType = AnswerActionType.TransitionToDialogScene,
                                AnswerActionModel = new TransitionAnswerActionModel
                                {
                                    ToDialogSceneId = 2,
                                    MainText = "Answer to 2"
                                }
                            },
                            new AnswerModel
                            {
                                AnswerActionType = AnswerActionType.TransitionToDialogScene,
                                AnswerActionModel = new TransitionAnswerActionModel
                                {
                                    ToDialogSceneId = 3,
                                    MainText = "Answer to 3"
                                }
                            },
                            new AnswerModel
                            {
                                AnswerActionType = AnswerActionType.TransitionToDialogScene,
                                AnswerActionModel = new TransitionAnswerActionModel
                                {
                                    ToDialogSceneId = 4,
                                    MainText = "Answer to 4"
                                }
                            },
                        }
                    },
                    new DialogSceneModel
                    {
                        Id = 2,
                        MainText = "Some text scene 2"
                    },
                    new DialogSceneModel
                    {
                        Id = 3,
                        MainText = "Some text scene 3"
                    },
                    new DialogSceneModel
                    {
                        Id = 4,
                        MainText = "Some text scene 4"
                    }
                }
            };
            
            File.WriteAllText( Application.persistentDataPath + "/test.dm", JsonConvert.SerializeObject(scenario, Formatting.Indented));
        }
    }
}
