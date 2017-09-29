using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OAuth;
using System.Web;
using System.Net;
using System.IO;
using TweetSharp;
using System.Diagnostics;
using Hammock;
using System.Xml;
using System.Drawing.Drawing2D;
using DiffuseDlgDemo;
using System.Security.Cryptography;
using AForge.Vision.Motion;
using AForge.Imaging;

namespace TweetMyEverything
{
    public partial class TweetMyEverything : Form//Abhinaba.TransDlg.TransDialog
    {
        private HalloForm _hallo;
        /// <summary>
        /// Timer that keeps track on how often we need to automatically take a screenshot, if that option is enabled.
        /// </summary>
        private Timer _timer;
        private System.Drawing.Image previousScreenshot;
        private bool doAutoScreeshot = false;
        public TweetMyEverything()
        {
            InitializeComponent();
            
            #region Unused Code
            // _hallo = new HalloForm();
           // _timer = new Timer() { Interval = 20, Enabled = true };
           // _timer.Tick += new EventHandler(Timer_Tick);
            #endregion
            
            
        }
        /// <summary>
        /// Delegate to write to the form, if we are in a diffirent thread.
        /// </summary>
        /// <param name="text"></param>
        public delegate void setTwitterPostText( string text );
        /// <summary>
        /// Modifies the tweet by appending an additional string to it. Most cases this is the twitpic URL
        /// </summary>
        /// <param name="text"></param>
        public void setTweet(string text)
        {
            tweetPost.Text += " "+text;
        }
        /// <summary>
        /// Takes a full screenshot and returns the the image
        /// </summary>
        /// <returns>image representing the screenshot</returns>
        private System.Drawing.Image takeScreenshot()
        {
            ScreenShotDemo.ScreenCapture sc = new ScreenShotDemo.ScreenCapture();
            System.Drawing.Image img = sc.CaptureScreen();
            img.Save("screenshot.jpg");
            return img;
        }
        /// <summary>
        /// What initiall happens when the main form is loaded and shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {         
            this.picture.Image = takeScreenshot();
            this.picture.Paint += new PaintEventHandler(picture_Paint);
        }

        #region Unused Code
        void Timer_Tick(object sender, EventArgs e) { Point pt = Cursor.Position; pt.Offset(-(_hallo.Width / 2), -(_hallo.Height / 2)); _hallo.Location = pt; if (!_hallo.Visible) { _hallo.Show(); } } 

        void picture_Paint(object sender, PaintEventArgs e)
        {
            //using (Font myFont = new Font("Arial", 14)) 
            //{ 
            //    e.Graphics.DrawString("Hello .NET Guide!", myFont, Brushes.Green, new Point(2, 2));
            //} 
           
        }
        #endregion

        public class HalloForm : Form { public HalloForm() { TopMost = true; ShowInTaskbar = false; FormBorderStyle = FormBorderStyle.None; BackColor = Color.LightGreen; TransparencyKey = Color.LightGreen; Width = 100; Height = 100; Paint += new PaintEventHandler(HalloForm_Paint); } void HalloForm_Paint(object sender, PaintEventArgs e) { e.Graphics.DrawEllipse(Pens.Black, (Width - 25) / 2, (Height - 25) / 2, 25, 25); } }
        /// <summary>
        /// Sends the twitter post, indirectly via a background thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
            // send message to twitter
            sendButton.Text = "Uploading...";
            sendButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
            

        }
        /// <summary>
        /// Restores gui controls to their previous state after a successful send.
        /// </summary>
        private void restoreSendButtonState()
        {
            sendButton.Text = "Send";
            sendButton.Enabled = true;
        }
        /// <summary>
        /// Resizes an image
        /// </summary>
        /// <param name="imgToResize">Image file to resize</param>
        /// <param name="size">new dimensions to resize to</param>
        /// <returns></returns>
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (System.Drawing.Image)b;
        }
        /// <summary>
        /// Main way to notify the user that something has happened. Allows a image to be use inline. The image is resized.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="image"></param>
        private void setStatus(string p, System.Drawing.Image image)
        {
            DiffuseDlgDemo.Notification notify = new DiffuseDlgDemo.Notification();
            notify.setMessage(p);
            if( image != null )
                notify.setImage(resizeImage(image, new Size(72,88)));
            notify.Show();
        }
        /// <summary>
        /// Determin the possibility of allowing the send button, based on how many characters the user has typed in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tweetPost_TextChanged(object sender, EventArgs e)
        {
            /* Determine how many characters the user has left*/
            int count_real = 140 - tweetPost.Text.Length;
            count.Text = "(" + count_real + ")";
                  
            /* Allow send if the characters total are less than 140 */
            if ((tweetPost.Text.Length <= 140) == false)
            {
                sendButton.Enabled = false;   
                
            }
            else
            {
                sendButton.Enabled = true;
         
            }
            /* Give a little visual clue that you are comming
             * to the end of your allocated 140 twitter characters */
            if (count_real < 10 && count_real > 0)
            {
                count.Font = new Font(count.Font, FontStyle.Bold);
            }
            else
            {
                count.Font = new Font(count.Font, FontStyle.Regular);
            }
        }
        /// <summary>
        /// Simple event that takes a screenshot and show it in the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
                this.picture.Image = takeScreenshot();
                this.WindowState = FormWindowState.Normal;
                this.Show();
        
        }
        /// <summary>
        /// Called when the user tries to close the main window of the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TweetMyEverything_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* If the main window is shown, closing it - only minimises it to the tray */
            if (this.Visible == true)
            {
                e.Cancel = true;
                this.Hide();
                // Start the timer.
                timer.Interval = ((int)intervalValue.Value) * 1000;
                
                /* As soon as the window closes, start the timer if it hasn't already been started. */
                if( timer.Enabled == false)
                    timer.Start();

            }
            else
            {
                /* If the quit signal came from elsewhere ie. when the form is not shown - it means we actually
                 want to quite this time.
                 */
                /* Lets do the right thing and stop the timer if its running.*/
                if( timer.Enabled == true)
                    timer.Stop(); 

                e.Cancel = false;
                
            }
        }
        /// <summary>
        /// Tray icon quit event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        #region Unused Image Formatting options
        private void stretchImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void centerImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picture.SizeMode = PictureBoxSizeMode.CenterImage;
        }
        #endregion
        
        /// <summary>
        /// The workhorse. Uploads last screenshot to TwitPic and appends its tiny url to current twitter post and then sends it to twitter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string consumerKey = "w3jFiS3GywLejyhbrCCw";
            string consumerSecret = "AEIA7rYdq6fzBVUMw1mG8phfVJZhc9oN081S8jfZ9kE";
            try
            {
                TwitterService service = new TwitterService(consumerKey, consumerSecret);
                OAuthRequestToken requestToken = service.GetRequestToken();
                Uri uri = service.GetAuthorizationUri(requestToken);
                Process.Start(uri.ToString());

                Verify verify = new Verify();
                /* Get verification code in the browser. */
                verify.ShowDialog();

                string verifier = verify.getCode();
                OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);
                service.AuthenticateWith(access.Token, access.TokenSecret);
                //IEnumerable<TwitterStatus> mentions = service.ListTweetsMentioningMe();

                /* Send screenshot to twitpic */

                TwitterService twitPic = new TwitterService(consumerKey, consumerSecret);
                twitPic.AuthenticateWith(access.Token, access.TokenSecret);

                RestRequest request = twitPic.PrepareEchoRequest();

                request.Path = "uploadAndPost.xml";
                request.AddFile("media", "screenshot" + DateTime.Now.ToString(), "screenshot.jpg", "image/jpeg");
                request.AddField("key", "847eac6c266777a6c036071272ec2636"); // <-- Sign up with TwitPic to get an API key
                request.AddField("message", tweetPost.Text);

                RestClient client = new RestClient { Authority = "http://api.twitpic.com/", VersionPath = "2" };
                RestResponse response = client.Request(request);
           
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(response.Content);
                XmlNode node = xml.SelectSingleNode("//url");
                string twitPicUrl = node.InnerText;


                /* Send to twitter */
                
                if (this.InvokeRequired == false)
                {
                    tweetPost.Text += " " + twitPicUrl;
                }
                else
                {
                   
                    this.Invoke(new setTwitterPostText(setTweet),new object[]{twitPicUrl});
                }
                
                if (sendButton.Enabled == true)
                {
                    IAsyncResult result = service.SendTweet(tweetPost.Text, (theTwitterStatus, theResponse) =>
                    {
                        if (theResponse.StatusCode == HttpStatusCode.OK)
                        {
                            
                        }
                    });


                }

            }
            catch (ArgumentNullException nullArgument)
            {
                backgroundWorker1.CancelAsync();
               
                return;
            }
        }
        /// <summary>
        /// When its done sending. This method is executed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (backgroundWorker1.CancellationPending == true)
            {
                setStatus("Failed to communicate.", null);
            }
            else
            {
                setStatus("Successfully sent.", picture.Image);
            }
            tweetPost.Clear();
            restoreSendButtonState();
        }
        /// <summary>
        /// Shows the credits dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Credits().Show();
        }
        /// <summary>
        /// Controls whether the app will take automatic screenshots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            intervalValue.Enabled = enableAutoCapture.Checked;
            doAutoScreeshot = enableAutoCapture.Checked;
        }
        /// <summary>
        /// When the time arives to take a screenshot, take a screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick_1(object sender, EventArgs e)
        {
            // cool, save a screenshot
            ScreenShotDemo.ScreenCapture sc = new ScreenShotDemo.ScreenCapture();
            System.Drawing.Image image = sc.CaptureScreen();

            string filename = DateTime.Now.ToString();
            filename = filename.Replace(":", "_");
            filename = filename.Replace(@"/", "_");
            /* If we took a screenshot previously, lets compare the diffirences and draw
             it onto the new image
             */
            if (previousScreenshot != null)
            {
                BlobCountingObjectsProcessing motionProcessing = new BlobCountingObjectsProcessing();
                motionProcessing.MinObjectsHeight = 80;
                motionProcessing.MinObjectsWidth = 80;                
                MotionDetector motionDetector = new MotionDetector(new SimpleBackgroundModelingDetector(), motionProcessing);                
                /*Load the previous frame.*/
                motionDetector.ProcessFrame(new Bitmap(previousScreenshot));
                /* Load the new/current frame and look for a change.*/
                if (motionDetector.ProcessFrame(new Bitmap(image)) > 0.02)
                {
                    /* Yes changed occured.*/
                    image.Save(filename + ".jpg");
                    Graphics gImage = Graphics.FromImage(image);
                    // Ok, lets try and identify all the objects that changed.
                    if (motionProcessing.ObjectsCount > 1)
                    {
                        BlobCounter bc = new BlobCounter();
                        /* Fect the objects from the changed frame*/
                        bc.ProcessImage(motionDetector.MotionDetectionAlgorthm.MotionFrame);
                        
                        foreach (Rectangle rect in bc.GetObjectsRectangles())
                        {
                            /* Outline the changes in the picture*/
                            gImage.DrawRectangle(Pens.Yellow, rect);
                           // gImage.FillRectangle(Brushes.Yellow, rect);
                            
                        }
                        /* make a new image with shows the diffirences between the previous
                         and current frame or picture.*/
                        image.Save(filename + "_changed_.jpg");
                       
                    }                    
                }
                image.Save(filename + ".jpg");
                previousScreenshot = image;
            }
            else
            {
                image.Save(filename + ".jpg");
                previousScreenshot = image;
            }
        }
    }
}
