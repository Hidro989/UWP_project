#pragma checksum "D:\UWP Project\FlashCard1\FlashCard1\Pages\CardPage\Dialog\AskLearnDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "32DEF1CDF342821A78930DC5DE766257C532FAA38F8A461F93A3F02D6146377C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlashCard1.Pages.CardPage.Dialog
{
    partial class AskLearnDialog : 
        global::Windows.UI.Xaml.Controls.ContentDialog, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Pages\CardPage\Dialog\AskLearnDialog.xaml line 1
                {
                    this.AskLearnDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 2: // Pages\CardPage\Dialog\AskLearnDialog.xaml line 50
                {
                    global::Windows.UI.Xaml.Controls.Button element2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element2).Click += this.Button_Click_1;
                }
                break;
            case 3: // Pages\CardPage\Dialog\AskLearnDialog.xaml line 57
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Button_Click;
                }
                break;
            case 4: // Pages\CardPage\Dialog\AskLearnDialog.xaml line 37
                {
                    global::Microsoft.UI.Xaml.Controls.RadioButtons element4 = (global::Microsoft.UI.Xaml.Controls.RadioButtons)(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButtons)element4).SelectionChanged += this.RadioButtons_SelectionChanged;
                }
                break;
            case 5: // Pages\CardPage\Dialog\AskLearnDialog.xaml line 25
                {
                    this.ComboboxMode = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ComboboxMode).SelectionChanged += this.ComboboxMode_SelectionChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

