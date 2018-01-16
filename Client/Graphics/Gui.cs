using GeonBit.UI;
using GeonBit.UI.Entities;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Client
{
    class Gui
    {
        public static List<Panel> Windows = new List<Panel>();

        public void Start()
        {
            CreateWindow_Login();
            CreateWindow_Register();
            CreateWindow_Teste();
        }

        public void CreateWindow(Panel panel)
        {
            Windows.Add(panel);
        }

        public void CreateWindow_Teste()
        { 
            //create entity's
            Panel panel = new Panel(new Vector2(400, 250), skin: PanelSkin.Default, anchor: Anchor.TopLeft, offset: new Vector2(10, 10));
            Icon icon = new Icon(IconType.Heart);
            panel.AddChild(icon);
            //panel.Scale = 0.7f;
            UserInterface.Active.AddEntity(panel);
            //create the window
            CreateWindow(panel);
        }

        public void CreateWindow_Login()
        {
            //create entity's
            Panel panel = new Panel(new Vector2(500, 430));
            Button btnLogin = new Button("Login");
            TextInput txtUser = new TextInput(false);
            TextInput txtPass = new TextInput(false);
            CheckBox chkUser = new CheckBox("Save Username?");
            Header header1 = new Header("Username", Anchor.TopCenter);
            Header header2 = new Header("Password", Anchor.AutoCenter);
            Label label1 = new Label("No Account yet? Then sign up here.", Anchor.AutoCenter);
            UserInterface.Active.AddEntity(panel);

            //entity settings
            txtUser.PlaceholderText = "enter username...";
            txtPass.PlaceholderText = "enter password...";

            //add entity
            panel.AddChild(header1);
            panel.AddChild(txtUser);
            panel.AddChild(header2);
            panel.AddChild(txtPass);
            panel.AddChild(chkUser);
            panel.AddChild(btnLogin);
            panel.AddChild(label1);

            //on click events
            btnLogin.OnClick += (Entity entity) =>
            {
                if (Globals.loginUsername == string.Empty | Globals.loginPassword == string.Empty)
                {
                    MessageBox.ShowMsgBox("No Input", "Please enter a valid username or password before trying to login!", new MessageBox.MsgBoxOption[]
                    {
                        new MessageBox.MsgBoxOption("Okay!" ,() => {return true; })
                    });
                }
                else
                {
                    ClientSendData.instance.SendLogin();
                }
            };

            label1.OnClick += (Entity entity) =>
            {
                MenuManager.ChangeMenu(MenuManager.Menu.Register);
            };

            //TextBox events
            txtUser.OnValueChange = (Entity textUser) => { Globals.loginUsername = txtUser.Value; };
            txtPass.OnValueChange = (Entity textPass) => { Globals.loginPassword = txtPass.Value; };

            //create the window
            CreateWindow(panel);
        }

        public void CreateWindow_Register()
        {
            //create entity's
            Panel panel = new Panel(new Vector2(500, 550));
            Button btnRegister = new Button("Register");
            Button btnBack = new Button("Back");
            TextInput txtUser = new TextInput(false);
            TextInput txtPass = new TextInput(false);
            TextInput txtPassRepeat = new TextInput(false);
            Header header1 = new Header("Username", Anchor.TopCenter);
            Header header2 = new Header("Password", Anchor.AutoCenter);
            Header header3 = new Header("Repeat Password", Anchor.AutoCenter);
            UserInterface.Active.AddEntity(panel);

            //entity settings
            txtUser.PlaceholderText = "enter username...";
            txtPass.PlaceholderText = "enter password...";
            txtPassRepeat.PlaceholderText = "repeat password...";

            //add entity
            panel.AddChild(header1);
            panel.AddChild(txtUser);
            panel.AddChild(header2);
            panel.AddChild(txtPass);
            //panel.AddChild(header3);
            //panel.AddChild(txtPassRepeat);
            panel.AddChild(btnRegister);
            panel.AddChild(btnBack);

            //on click events
            btnRegister.OnClick += (Entity entity) =>
            {
                if (Globals.regUsername == string.Empty | Globals.regPassword == string.Empty)
                {
                    MessageBox.ShowMsgBox("No Input", "Please enter a valid username or password before trying to login!", new MessageBox.MsgBoxOption[]
                    {
                        new MessageBox.MsgBoxOption("Okay!" ,() => {return true; })
                    });
                }
                else
                {
                    ClientSendData.instance.SendRegister();
                }
            };

            btnBack.OnClick += (Entity entity) =>
            {
                MenuManager.ChangeMenu(MenuManager.Menu.Login);
            };

            //TextBox events
            txtUser.OnValueChange = (Entity textUser) => { Globals.regUsername = txtUser.Value; };
            txtPass.OnValueChange = (Entity textPass) => { Globals.regPassword = txtPass.Value; };

            //create the window
            CreateWindow(panel);
        }
    }
}
