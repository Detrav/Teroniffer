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
        bool visable = false;
        ITeraClient parent;

        public void changeVisible()
        {
            if (visable)
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
            visable = false;
        }

        public void load(ITeraClient parent)
        {
            this.parent = parent;
            parent.onPacketArrival += parent_onPacketArrival;
            window = new MainWindow();
        }

        void parent_onPacketArrival(object sender, TeraApi.Events.PacketArrivalEventArgs e)
        {
            window.parsePacket(e.packet);
        }

        public void show()
        {
            window.Show();
            visable = true;
        }

        public void unLoad()
        {
            window.Close();
        }

        public static byte[] getModIcon()
        {
            return extractResource("Detrav.Teroniffer.Icon.png");
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
