[1mdiff --git a/ParsnipWebsite/View.aspx.cs b/ParsnipWebsite/View.aspx.cs[m
[1mindex 6639944..75043c3 100644[m
[1m--- a/ParsnipWebsite/View.aspx.cs[m
[1m+++ b/ParsnipWebsite/View.aspx.cs[m
[36m@@ -243,13 +243,14 @@[m [mnamespace ParsnipWebsite[m
                     youtube_video_container.Visible = false;[m
                 }[m
 [m
[31m-                if (mediaView.Video.Status != null)[m
[32m+[m[32m                var mediaStatus = mediaView.Video?.Status ?? mediaView.YoutubeVideo?.Status;[m
[32m+[m[32m                if (mediaStatus != null)[m
                 {[m
[31m-                    if (mediaView.Video.Status.Equals(MediaStatus.Unprocessed))[m
[32m+[m[32m                    if (mediaStatus.Equals(MediaStatus.Unprocessed))[m
                         unprocessed.Visible = true;[m
[31m-                    else if (mediaView.Video.Status.Equals(MediaStatus.Processing))[m
[32m+[m[32m                    else if (mediaStatus.Equals(MediaStatus.Processing))[m
                         processing.Visible = true;[m
[31m-                    else if (mediaView.Video.Status.Equals(MediaStatus.Error))[m
[32m+[m[32m                    else if (mediaStatus.Equals(MediaStatus.Error))[m
                         error.Visible = true;[m
                 }[m
 [m
