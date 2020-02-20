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
    /// Interaction logic for UCShareMessage.xaml
    /// </summary>
    public partial class UCShareMessage : UserControl
    {
        public UCShareMessage()
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

        private void EncryptSelfMessage_Click(object sender, RoutedEventArgs e)
        {

            String data = StringFromRichTextBox(MessageBoxData);
            HandyControl.Controls.MessageBox.Success(data, "Can Read from Text Box");
            if (NewPass_box.Text == "")
            {
                HandyControl.Controls.MessageBox.Error("New Shareable Password Needed", "No new Password Found");
            }
            else
            {


                //  HandyControl.Controls.MessageBox.Warning(data, "Registration Error");
                Tools.PutShareMessage("                  " + StringFromRichTextBox(MessageBoxData));
                Tools.putCurrentPassword(NewPass_box.Text.ToString());
                HandyControl.Controls.MessageBox.Success(NewPass_box.Text.ToString(), "Can Read from Pass Box");
                Tools.NumCreator();
                Tools.resetCurrentPassword();
                Tools.PutEnShareMessage();

                HandyControl.Controls.MessageBox.Success(Tools.SaveFileDialogShare(), "File has been Locked");
            }
        }

        private void DecryptSelfMessage_Click(object sender, RoutedEventArgs e)
        {
            Tools.putCurrentPassword(NewPass_box.Text.ToString());
            Tools.NumCreator();
            Tools.resetCurrentPassword();
            if (Tools.GetDeSelfMessage())
                MessageBoxData.Document.Blocks.Add(new Paragraph(new Run(Tools.GetDeSelfMessageVal())));

        }
    }
}
