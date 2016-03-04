using System.Drawing;
using System.Windows.Forms;

namespace AxialFluxGeneratorDesigner.Gui
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlToImage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        public static void GenerateImage(Control panel)
        {
            //Save control images for documentation purposes
            var image = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
            panel.DrawToBitmap(image, panel.ClientRectangle);
            image.Save(panel.Name + ".png");
        }
    }
}
