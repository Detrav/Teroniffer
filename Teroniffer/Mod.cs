using Detrav.TeraApi.Interfaces;
using Detrav.Teroniffer.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer
{
    class Mod : ITeraMod
    {
        MainWindow window;
        ITeraClient parent;

        public void changeVisible()
        {
            if (window.IsVisible)
                hide();
            else
                show();
        }

        public void configManager(IConfigManager configManager)
        {
            //throw new NotImplementedException();
        }

        public void hide()
        {
            window.Hide();
        }

        public void load(ITeraClient parent)
        {
            window = new MainWindow();
            this.parent = parent;
            parent.onPacketArrival += parent_onPacketArrival;
            parent.onTick += parent_onTick;
            show();
        }

        void parent_onTick(object sender, EventArgs e)
        {
            window.doEvents();
        }

        void parent_onPacketArrival(object sender, TeraApi.Events.PacketArrivalEventArgs e)
        {
            window.parsePacket(e.packet);
        }

        public void show()
        {
            window.Show();
        }

        public void unLoad()
        {
            window.close = true;
            window.Close();
        }

        public static byte[] getModIcon()
        {
            return extractResource("Detrav.Teroniffer.Icon.jpg");
        }

        public static byte[] extractResource(string filename)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            using (System.IO.Stream resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
    }
}
