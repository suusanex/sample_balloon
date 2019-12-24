using System;
using System.Collections.Generic;
using System.Drawing;
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
using Hardcodet.Wpf.TaskbarNotification;

namespace sample_balloon_WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private NotifyBalloon m_NotifyBalloon;
        private TaskbarIcon m_TaskbarIcon;

        private void OnBtnBalloon(object sender, RoutedEventArgs e)
        {
            m_NotifyBalloon.NotifyShow(DateTime.Now.ToString());
        }

        private void OnBtnCreateTaskTray(object sender, RoutedEventArgs e)
        {
            if (m_TaskbarIcon != null)
            {
                MessageBox.Show("Exist");
                return;
            }

            m_TaskbarIcon = new TaskbarIcon();

            using (var iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/Icon1.ico", UriKind.RelativeOrAbsolute))?.Stream)
            {
                if (iconStream == null)
                {
                    throw new Exception($"Icon Read Fail");
                }

                m_TaskbarIcon.Icon = new Icon(iconStream);
            }

            m_NotifyBalloon = new NotifyBalloon(m_TaskbarIcon);
        }
    }
}
