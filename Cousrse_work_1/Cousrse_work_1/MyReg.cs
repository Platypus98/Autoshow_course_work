using Microsoft.Win32;
using System;
using System.Windows.Forms;

// Класс содержит три метода для работы с системным реестром.
public class MyReg
{
    public static string subKeyName = "Hotel";         // Имя подраздела.
    public static string valueName = "File";          // Имя значения (ключ).

    static RegistryKey localUser = Registry.CurrentUser;   // Раздел реестра
    static RegistryKey subKey;                  // Ссылка для подраздела реестра.


    //---------------------------------------------------------
    // Метод читает значение по ключу File в подразделе Hotel.
    // Возвращаемый результат: 
    //     "значение", если значение прочитано; 
    //     "", если подраздела или значения нет или ошибка.
    public static string ValueGet()
    {
        try
        {
            subKey = localUser.OpenSubKey(subKeyName);

            if (subKey != null)
            {
                object obj = subKey.GetValue(valueName);

                if (obj != null)  // Параметр сохранен.
                {
                    subKey.Close();
                    return (string)obj;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Получить имя из реестра не удалось. \n\n" + ex.Message,
                "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        return "";
    }

    //---------------------------------------------------------
    // Метод записывает значение по ключу File в подразделе Hotel.
    // Параметр - имя файла без пути.
    // Возвращаемый результат: 
    //     true - успешно записано; 
    //     false - не записано из-за ошибки.
    public static bool ValueSet(string value)
    {
        try
        {
            // Записать в реестр имя выбранного файла в качестве текущего.
            subKey = localUser.OpenSubKey(subKeyName, true);

            if (subKey == null)
                subKey = localUser.CreateSubKey(subKeyName);

            subKey.SetValue(valueName, value);
            subKey.Close();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Записать имя в реестр не удалось. \n\n" + ex.Message,
                "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }


    //---------------------------------------------------------
    // Метод удаляет значение по ключу File в подразделе Hotel.
    // Возвращаемый результат: 
    //     true - успешно удалили подраздел Hotel и значение File; 
    //     false - не удалили из-за ошибки.

    public static bool SubKeyDel()
    {
        try
        {
            // Удалить из реестра подраздел.
            subKey = localUser.OpenSubKey(subKeyName, true);

            subKey.DeleteSubKey("");
            subKey.Close();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не удалось удалить из реестра подраздел 'Hotel'. \n\n" + ex.Message,
                "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            subKey.Close();
            return false;
        }
    }
}