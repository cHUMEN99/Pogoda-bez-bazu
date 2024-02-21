using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace App2
{
    public partial class MainPage : ContentPage
    {

        const string API = "d387f8ef5e79e8b42b9fc53ccf00aa6f";
        public MainPage()
        {
            InitializeComponent();


        }


        private async void getWeather1_Clicked(object sender, EventArgs e)
        {
            string city = userInput.Text.Trim();
            if (city.Length <= 2)
            {
                await DisplayAlert("Помилка!", "Менше 2 символів!", "OK");
                return;
            }
            else if (city.Length >= 20)
            {
                await DisplayAlert("Помилка!", "Більше 20 символів!", "OK");
                return;
            }

            try
            {
                HttpClient client = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API}&units=metric";
                string response = await client.GetStringAsync(url);

                if (!string.IsNullOrEmpty(response))
                {
                    var json = JObject.Parse(response);
                    string temp = json["main"]["temp"].ToString();
                    resultLabel.Text += $"Температура зараз в мість {city} " + temp;
                    string mintemp = json["main"]["temp_min"].ToString();
                    resultLabel.Text += $"Мінімальна температура яка була в місті {city}" + mintemp;
                }
                else
                {
                    await DisplayAlert("Помилка!", "Отримана порожня відповідь від сервера", "OK");
                }
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Помилка!", "Помилка під час виконання запиту: " + ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка!", "Сталася невідома помилка: " + ex.Message, "OK");
            }
        }
    }
}
