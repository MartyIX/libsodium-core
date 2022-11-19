namespace Sodium.MacCatalyst.Sample;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
    public override UIWindow? Window {
        get;
        set;
    }

    public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
    {
        // create a new window instance based on the screen size
        Window = new UIWindow (UIScreen.MainScreen.Bounds);


        var key = new byte[]
        {
            0x42, 0x90, 0xbc, 0xb1, 0x54, 0x17, 0x35, 0x31, 0xf3, 0x14, 0xaf,
            0x57, 0xf3, 0xbe, 0x3b, 0x50, 0x06, 0xda, 0x37, 0x1e, 0xce, 0x27,
            0x2a, 0xfa, 0x1b, 0x5d, 0xbd, 0xd1, 0x10, 0x0a, 0x10, 0x07
        };

        var nonce = new byte[]
        {
        0xcd, 0x7c, 0xf6, 0x7b, 0xe3, 0x9c, 0x79, 0x4a, 0x79, 0xc0, 0xd1, 0x10
        };

        var ad = new byte[]
        {
        0x87, 0xe2, 0x29, 0xd4, 0x50, 0x08, 0x45, 0xa0, 0x79, 0xc0
        };

        var m = new byte[]
        {
        0x86, 0xd0, 0x99, 0x74, 0x84, 0x0b, 0xde, 0xd2, 0xa5, 0xca
        };

        string text;

        if (SecretAeadAes.IsAvailable)
        {
            var encrypted = SecretAeadAes.Encrypt(m, nonce, key, ad);
            var decrypted = SecretAeadAes.Decrypt(encrypted, nonce, key, ad);
            text = $"Equal? {m.SequenceEqual(decrypted)}";
        }
        else
        {
            text = "Missing AES support";
        }

        // create a UIViewController with a single UILabel
        var vc = new UIViewController();
        vc.View!.AddSubview(new UILabel (Window!.Frame) {
            BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Hello, Mac Catalyst!",
            AutoresizingMask = UIViewAutoresizing.All,
        });
        Window.RootViewController = vc;

        // make the window visible
        Window.MakeKeyAndVisible();

        return true;
    }
}

