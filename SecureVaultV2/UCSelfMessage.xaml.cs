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

namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for UCSelfMessage.xaml
    /// </summary>
    public partial class UCSelfMessage : UserControl
    {
        public UCSelfMessage()
        {
            InitializeComponent();
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

        //richTextBox1.Document.Blocks.Clear();
        //richTextBox1.Document.Blocks.Add(new Paragraph(new Run("Text")));

        private void EncryptSelfMessage_Click(object sender, RoutedEventArgs e)
        {
           // MessageBoxData

                String data = StringFromRichTextBox(MessageBoxData);
          //  HandyControl.Controls.MessageBox.Warning(data, "Registration Error");
            Tools.PutSelfMessage(StringFromRichTextBox(MessageBoxData));
            Tools.NumCreator();
            Tools.PutEnSelfMessage();
            
            HandyControl.Controls.MessageBox.Success(Tools.SaveFileDialog(), "File has been Locked");
            // System.IO.File.AppendAllText("Newchup.txt", like.ToString());

            // MessageBoxData.Document.Blocks.Add(new Paragraph(new Run(Tools.GetEnSelfMessage())));


        }

        private void DecryptSelfMessage_Click(object sender, RoutedEventArgs e)
        {
            Tools.NumCreator();
            Tools.GetDeSelfMessage();
            MessageBoxData.Document.Blocks.Add(new Paragraph(new Run(Tools.GetDeSelfMessageVal())));
        }
    }
}
