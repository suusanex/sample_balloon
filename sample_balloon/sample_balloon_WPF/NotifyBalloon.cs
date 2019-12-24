using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace sample_balloon_WPF
{
    public class NotifyBalloon
    {
        public void NotifyShow(string message)
        {
            var item = new BalloonData
            {
                Message = message,
                Title = string.Empty
            };

            m_ShowWaitBalloonData.Enqueue(item);

            if (m_ShowWaitBalloonData.Count <= 1)
            {
                Application.Current.Dispatcher?.Invoke(() => m_TaskTray.ShowBalloonTip(item.Title, item.Message, BalloonIcon.None));
            }
        }

        public NotifyBalloon(TaskbarIcon taskTray)
        {
            m_TaskTray = taskTray;
            m_TaskTray.TrayBalloonTipClosed += OnBalloonTipClosed;
        }


        class BalloonData
        {
            public string Message;
            public string Title;
        }

        /// <summary>
        /// 表示待ちバルーンデータを入れるキュー。現在表示中のデータもキューの中に残し、バルーンを閉じたらキューから消す。
        /// </summary>
        private readonly ConcurrentQueue<BalloonData> m_ShowWaitBalloonData = new ConcurrentQueue<BalloonData>();

        private readonly TaskbarIcon m_TaskTray;

        public void OnBalloonTipClosed(object sender, RoutedEventArgs e)
        {
            //今閉じたバルーンの情報がキューに入っているので、削除。続けてキューにデータが残っている場合は、次を表示。
            m_ShowWaitBalloonData.TryDequeue(out _);

            if (m_ShowWaitBalloonData.TryPeek(out var item))
            {
                Application.Current.Dispatcher?.Invoke(() => m_TaskTray.ShowBalloonTip(item.Title, item.Message, BalloonIcon.None));
            }
        }
    }
}
