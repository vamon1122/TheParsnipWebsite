﻿using ParsnipData.Accounts;
using ParsnipData.Media;
using System.Configuration;
using System.Diagnostics;
using System.Web.Services;
using System.Web;
using System;
using System.Linq;

namespace ParsnipWebsite
{
    public class MediaViewPage : ParsnipPage
    {
        [WebMethod]
        public static void OnMediaCenterScreen(string containerId, string bodyId)
        {
            var timer = new Stopwatch();
            timer.Start();
            var thisViewId = Guid.NewGuid();
            var session = HttpContext.Current.Session;
            session[$"{bodyId}_CurrentViewId"] = thisViewId.ToString();
            var splitContainerId = containerId.Split('_');
            if (splitContainerId.Length < 2 || splitContainerId.Last() == "thumbnail")
            {
                session[$"{bodyId}_CurrentViewMediaId"] = null;
                Debug.WriteLine($"Video focused (Ignoring)");
                return;
            }
            StartImageViewTimer(thisViewId, new MediaId(splitContainerId.Last()), ParsnipData.Accounts.User.LogIn());
            void StartImageViewTimer(Guid viewId, MediaId mediaId, User loggedInUser)
            {
                session[$"{bodyId}_CurrentViewMediaId"] = mediaId;
                Debug.WriteLine($"Image focused ({mediaId} is an image. Starting timer...)");
                System.Timers.Timer minInsertViewTimer;
                var imageViewThreshold = TimeSpan.FromMilliseconds(Convert.ToInt16(ConfigurationManager.AppSettings["InsertImageViewAfterMilliseconds"]));
                minInsertViewTimer = new System.Timers.Timer(imageViewThreshold.TotalMilliseconds);
                minInsertViewTimer.Elapsed += (sender, e) => OnImageViewThresholdMet();
                minInsertViewTimer.AutoReset = false;
                minInsertViewTimer.Enabled = true;

                void OnImageViewThresholdMet()
                {
                    if (viewId.ToString() == session[$"{bodyId}_CurrentViewId"]?.ToString())
                    {
                        System.Timers.Timer checkViewStillInFocus;
                        checkViewStillInFocus = new System.Timers.Timer(1);
                        checkViewStillInFocus.Elapsed += (sender, e) => OnViewStillInFocus();
                        checkViewStillInFocus.AutoReset = true;
                        checkViewStillInFocus.Enabled = true;

                        void OnViewStillInFocus()
                        {
                            if (viewId.ToString() != session[$"{bodyId}_CurrentViewId"]?.ToString())
                            {
                                checkViewStillInFocus.Close();
                                timer.Stop();

                                TimeSpan timeTaken = timer.Elapsed;
                                var tempMedia = new Media() { Id = mediaId };
                                tempMedia.View(loggedInUser, true, imageViewThreshold, timer.Elapsed);
                                Debug.WriteLine($"View inserted ({mediaId} was viewed continuously for {timeTaken.TotalSeconds} seconds)");
                                return;
                            }
                        }
                    }
                    else Debug.WriteLine($"View NOT inserted ({mediaId} was NOT viewed continuously for {imageViewThreshold.TotalSeconds} seconds)");
                }
            }
        }

        [WebMethod]
        public static void OnMediaUnFocused(string feedback, string unfocusedId) => Data.OnMediaUnFocused(feedback, unfocusedId);

        [WebMethod(EnableSession = true)]
        public static void OnMediaReFocused(string bodyId, string feedback)
        {
            var session = HttpContext.Current.Session;
            //session["CurrentViewId"] = Guid.NewGuid();
            if(session[$"{bodyId}_CurrentViewMediaId"] == null && session[$"{bodyId}_CurrentUnfocusedViewId"] == null)
            {
                Debug.WriteLine($"There was no media to re-focus");
            }
            else
            {
                Debug.WriteLine($"Refocusing media... ({feedback})");
                OnMediaCenterScreen("control_" + session[$"{bodyId}_CurrentUnfocusedViewId"].ToString(), bodyId);
            }
        }

        [WebMethod]
        public static void OnClose() => Debug.WriteLine("Tab closed");

        [WebMethod]
        public static void OnMenuOpenMediaNotReFocused() => Debug.WriteLine("Menu is open! Media not refocused.");

        [WebMethod]
        public static void OnMenuOpenMediaNotFocused() => Debug.WriteLine("Menu is open! Media not focused.");
    }
}
