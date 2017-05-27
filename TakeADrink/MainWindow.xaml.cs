using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls; // To create a non-ancient looking MetroUI
using Newtonsoft.Json; // so we can parse JSON
using System.Timers; // eh... timers... I guess?
using System.Threading; // multithreading is handydandy
using System.IO;
using Newtonsoft.Json.Linq;

namespace TakeADrink {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        System.Timers.Timer timer = new System.Timers.Timer(); // create our timer
        bool busy = false; // true if already updating, false if idle (to prevent updating multiple times at the same time)
        System.Threading.Thread syncThread = null; // create a new thread for updating the data (so we don't lock up the main thread)

        public MainWindow() {
            InitializeComponent();
            this.Title = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            this.label7.Content = Properties.Settings.Default.currentCity + " (" + Properties.Settings.Default.currentCountry + ")";
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed); // set the timer tick event
            timer.Interval = Properties.Settings.Default.locationUpdateDelay; // set the timer interval
            timer.Enabled = true; // Enable the timer
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e) {
            timer.Start(); // Start the timer
            timer_Elapsed(null, null);
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e) {
            syncThread = new Thread(new ThreadStart(UpdateData));
            syncThread.Start();
        }

        void UpdateData() {
            if (!busy) {
                // Only do this if we aren't updating already
                busy = true;  // we're busy now! Come back later!
                Console.WriteLine("Updating location data...");
                this.Dispatcher.Invoke(() => {label_Status.Content = "Updating IP...";}); // Update the status label
                //Properties.Settings.Default.currentIP = GetIP(); // Get the current IP and put it into the settings
                this.Dispatcher.Invoke(() => {label_Status.Content = "Updating Location...";}); // Update the status label
                string[] currentLocation = GetLocation(Properties.Settings.Default.currentIP); // Get the current Location
                Properties.Settings.Default.currentCountry = currentLocation[0]; // put the current country in the settings
                Properties.Settings.Default.currentCity = currentLocation[1]; // put the current city in the settings

                this.Dispatcher.Invoke(() => {label_Status.Content = "Updating Weather...";}); // Update the status label
                string[] Weather = GetWeather(Properties.Settings.Default.currentCity, Properties.Settings.Default.currentCountry, Properties.Settings.Default.APIKey, "Metric"); // Get the current weather with all the data we just fetched

                /*
                 * Update the UI Labels
                 */
                this.Dispatcher.Invoke(() => { label7.Content = Properties.Settings.Default.currentCity + " (" + Properties.Settings.Default.currentCountry + ")"; }); // set the location label
                this.Dispatcher.Invoke(() => { label8.Content = Weather[3]; }); // set the main weather label
                this.Dispatcher.Invoke(() => { label9.Content = Weather[0] + "°C"; }); // set the temperature label
                this.Dispatcher.Invoke(() => { label10.Content = Weather[1] + "%"; }); // set the humidity label
                this.Dispatcher.Invoke(() => { label11.Content = Weather[2] + "hPa"; }); // set the Pressure label
                this.Dispatcher.Invoke(() => { label12.Content = Weather[4] + "m/S (" + Weather[5] + "°)"; }); // set the Wind label

                Console.WriteLine("Finished Updating");
                this.Dispatcher.Invoke(() => {label_Status.Content = "";}); // Update the status label
                Properties.Settings.Default.Save(); // Save the settings
                busy = false; // we're not busy anymore, we can now start the cycle again
            } else {
                // If we already are updating
                Console.WriteLine("Already busy... waiting...");
            }
        }

        private static string GetIP() {
            string url = "http://checkip.dyndns.org"; // define the URL from which to get the IP (duh)
            try {
                /*
                 * Try this crap first
                 */
                System.Net.WebRequest req = System.Net.WebRequest.Create(url); // create a webrequest
                System.Net.WebResponse resp = req.GetResponse(); // get the request response
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()); // read the request response
                string response = sr.ReadToEnd().Trim(); // trim the HTML response
                string[] a = response.Split(':'); // split the response into an array with 2 objects (example IP shown): [0] => "Current IP Address", [1] => "127.0.0.1"
                string a2 = a[1].Substring(1); // strip the <html><body> tag from the beginning
                string[] a3 = a2.Split('<'); // split the leftovers into an array with 2 objects (example IP shown): [0] => "127.0.0.1", [1] => /body></html>
                string currentIP = a3[0]; // put the IP into a string so we can use it to get the location later
                return currentIP; // return the IP
            } catch {
                /*
                 * If something goes wrong, just return the saved values
                 */
                return Properties.Settings.Default.currentIP;  // if something goes wrong, just return the last from the settings
            }
        }

        private static string[] GetLocation(String IP){
            try { 
                /*
                 * Try this crap first
                 */
                String url = "https://freegeoip.net/json/" + IP; // define the URL from which to get the location with the IP given as function argument
                System.Net.WebRequest req = System.Net.WebRequest.Create(url); // create a webrequest
                System.Net.WebResponse resp = req.GetResponse(); // get the request response
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()); // read the request response
                String json = sr.ReadToEnd().Trim(); // trim the JSON response
                dynamic Data = JsonConvert.DeserializeObject(json); // deserialise the JSON response
                string[] Location = new string[2]; // define an array with maximum size of 2
                Location[0] = Data.country_code; // put the country code in the array
                Location[1] = Data.city; // put the city in the array
                return Location; // return the array
            } catch {
                /*
                 * If something goes wrong, just return the saved values
                 */
                string[] Location = new string[2]; // define an array with maximum size of 2
                Location[0] = Properties.Settings.Default.currentCountry; // put the country code in the array
                Location[1] = Properties.Settings.Default.currentCity; // put the city in the array
                return Location;  // if something goes wrong, just return the last from the settings
            }
        }

        private static string[] GetWeather(String City, String Country, String APIKey, String Units) {
            try {
                String url = "http://api.openweathermap.org/data/2.5/weather?q=" + City + "," + Country + "&units=" + Units + "&APPID=" + APIKey; // define the URL to get the weather from with the arguments given to the function
                Console.WriteLine(url);
                System.Net.WebRequest req = System.Net.WebRequest.Create(url); // create a webrequest
                System.Net.WebResponse resp = req.GetResponse(); // get the request response
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()); // read the request response
                String json = sr.ReadToEnd().Trim(); // trim the JSON response
                dynamic Data = JObject.Parse(json); // deserialise the JSON response
                string[] Weather = new string[8]; // define an array
                Weather[0] = Data.main.temp; // put the temperature in the array
                Weather[1] = Data.main.humidity; // put the humidity in the array
                Weather[2] = Data.main.pressure; // put the airpressure in the array

                // this is a bit of a hack job, if you don't like it, then fix it :D
                dynamic Data_Weather = "";
                foreach (var item in Data.weather) {
                    Data_Weather = JObject.Parse(item.ToString()); // deserialise the JSON response
                }

                Weather[3] = Data_Weather.main; // put the main weather in the array
                Weather[4] = Data.wind.speed; // put the windspeed in the array
                Weather[5] = Data.wind.deg; // put the wind direction in the array
                Weather[6] = Data.sys.sunrise; // put the sunrise in the array
                Weather[7] = Data.sys.sunset; // put the sunset in the array
                return Weather; // return the array
            } catch {
                string[] Weather = new string[8]; // define an array
                Weather[0] = ""; // put the temperature in the array
                Weather[1] = ""; // put the humidity in the array
                Weather[2] = ""; // put the airpressure in the array
                Weather[3] = ""; // put the main weather in the array
                Weather[4] = ""; // put the windspeed in the array
                Weather[5] = ""; // put the wind direction in the array
                Weather[6] = ""; // put the sunrise in the array
                Weather[7] = ""; // put the sunset in the array
                return Weather;
            }
            
        }

        private void SP_Contribute_MouseDown(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/FinlayDaG33k/TakeADrink"); // Open up the github repo in the default browser
        }

        private void SP_Settings_MouseDown(object sender, MouseButtonEventArgs e) {
            settings settings = new settings(); // create a new instance for the settings window
            settings.ShowDialog(); // show dat settings window
        }

        private void SP_About_MouseDown(object sender, MouseButtonEventArgs e) {
            about about = new about(); // create a new instance for the settings window
            about.ShowDialog(); // show dat settings window
        }
    }
}
