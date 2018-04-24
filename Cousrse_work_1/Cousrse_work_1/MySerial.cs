using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

// Класс содержит два метода: сериализации и десериализации
// Методам передаются статические поля класса Form1:
//  Form1.pathCurFile - относительный путь к файлу:
//  Form1.list - список объектов с данными.
//
// В вызовах методов определен тип данных ClientClass.  
class MySerial<T>
{
    static XmlSerializer xs = new XmlSerializer(typeof(T));
    static FileStream fs = null;
    //-------------------------------------------------------------
    // Метод СЕРИАЛИЗУЕТ список типа Т объектов (сохраняет в XML-файле).
    // параметры: T - тип списка;
    //            file - выходной XML-файл;
    //            list - коллекция типа List<T>.
    // Возвращаемое значение: true - норма, false - не сериализовано.
    public static bool Serialize(string file, T list)
    {
        try
        {
            fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Read);
            // Сохраним список в XML-файле
            xs.Serialize(fs, list);
            fs.Close();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка XML-сериализации списка в файл. \n\n" + ex.Message,
                   "Ошибка вывода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (fs != null)
                fs.Close();
            return false;
        }
    }

    //-------------------------------------------------------------
    // Метод ДЕСЕРИАЛИЗУЕТ список типа Т объектов (читает из XML-файла).
    // параметры: T - тип списка;
    //            file - исходный XML-файл.
    // Возвращаемое значение: список - норма, null - не десериализовано

    public static T Deserialize(string file)
    {
        T list;
        try
        {
            fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            list = (T)xs.Deserialize(fs);
            fs.Close();
            return list;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка XML-десериализации списка из файл. \n\n" + ex.Message,
                   "Ошибка чтения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (fs != null)
                fs.Close();
            return default(T);
        }
    }
}