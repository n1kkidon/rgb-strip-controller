namespace ApiServices.Services;

public class RGB
{
    public const string Topic = "dev_sub_ac:d8:29:1f:4b:ef";
    public static readonly byte[] on = { 250, 35, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 34, 251 };
    public static readonly byte[] off = { 250, 36, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 37, 251 };
    public static readonly byte[] firstLoginMsg = { 252, 240, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 253 };
    public static readonly byte[] secondLoginMsg = { 204, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 15, 221 };
    private readonly byte[] BaseArray = { 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 15, 107, 41 };
    private readonly byte[] currColors = { 0, 0, 0 };
    public int Red { get { return currColors[0]; } set { currColors[0] = Bytetify(value); SetValue(currColors[0], 1); } }
    public int Green { get { return currColors[1]; } set { currColors[1] = Bytetify(value); SetValue(currColors[1], 3); } }
    public int Blue { get { return currColors[2]; } set { currColors[2] = Bytetify(value); SetValue(currColors[2], 5); } }
    public byte[] WithColors => BaseArray;
    private float brightness = 1;
    public static bool LED_IS_ON { get; set; }
    public string GetHex() => $"#{Red:X2}{Green:X2}{Blue:X2}";

    public int Brightness
    {
        get { return (int)(brightness * 100); }
        set
        {
            if (value > 100)
                brightness = 1;
            else if (value < 0)
                brightness = 0;
            else
                brightness = (float)value / 100;
            SetRGB(Red, Green, Blue);
        }
    }

    public RGB()
    {
        Red = 255;
        Green = 255;
        Blue = 255;
        Brightness = 100;

        try
        {
            var lines = File.ReadAllLines("last.txt");
            SetRGB(int.Parse(lines[0]), int.Parse(lines[1]), int.Parse(lines[2]));
            LED_IS_ON = bool.Parse(lines[3]);
        }
        catch
        {
            Console.WriteLine("Retrieving settings failed!");
        }
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
    }

    private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
    {
        var f = File.CreateText("last.txt");
        f.WriteLine(Red);
        f.WriteLine(Green);
        f.WriteLine(Blue);
        f.WriteLine(LED_IS_ON);
        f.Close();
    }

    public static byte[] LoginMsg3
    {
        get
        {
            byte[] year = new byte[16];
            var date = DateTime.Now;
            year[0] = 176;
            year[1] = (byte)(date.Year % 256);
            year[2] = (byte)(date.Year / 256);
            year[3] = (byte)date.Month;
            year[4] = (byte)date.Day;
            year[5] = (byte)date.Hour;
            year[6] = (byte)date.Minute;
            year[7] = (byte)date.Second;
            year[8] = 1;
            year[9] = 240;
            year[10] = 0;
            year[11] = 0;
            year[12] = 0;
            year[13] = 0;
            year[14] = 5; //dont know what this one is
            year[15] = 177;
            return year;
        }
    }
    public void SetRGB(int r, int g, int b)
    {
        Red = r;
        Green = g;
        Blue = b;
        SetValue(currColors[0], 1);
        SetValue(currColors[1], 3);
        SetValue(currColors[2], 5);
    }

    public void SetRGB(string hex)
    {
        string parts = hex.Substring(1, 2);
        string parts1 = hex.Substring(3, 2);
        string parts2 = hex.Substring(5, 2);
        int decValue = Convert.ToInt32(parts, 16);
        int decValue1 = Convert.ToInt32(parts1, 16);
        int decValue2 = Convert.ToInt32(parts2, 16);

        SetRGB(decValue, decValue1, decValue2);
    }

    private static byte Bytetify(int b)
    {
        if (b > 255)
            b = 255;
        else if (b < 0)
            b = 0;
        return (byte)b;
    }
    private void SetValue(int x, int index)
    {
        var nval = x * brightness;
        x = (int)nval * 39;
        int rem = x % 255;
        int over = x / 255;
        BaseArray[index] = (byte)rem;
        BaseArray[index + 1] = (byte)over;
    }
}