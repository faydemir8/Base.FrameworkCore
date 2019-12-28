using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Base.Framework.Core.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// <see cref="IDictionary{TKey,TValue}"/> olarak gönderilen <paramref name="this"/> nesnesinin <see cref="ExpandoObject"/>'e çevirilmesini sağlar. 
        /// <see cref="ExpandoObject"/> olarak dönen değer dynamic bir değişken içerisine atılırsa, bu değişken üzerinden <paramref name="this"/> içerisindeki bir değere sanki bir nesnenin property'sini çağırıyormuş
        /// gibi Key'i üzerinden rahatlıkla ulaşılabilir. Örneğin Dictionary içerisinde "SomeProperty" keyi üzerinden "SomeValue" değeri atandıysa, dynamic değişken üzerinden nesne.SomeProperty şeklinde çağırılarak
        /// "SomeValue" değerine ulaşılabilir.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ExpandoObject ToDynamic(this IDictionary<string, object> @this)
        {
            if (@this == null) return null;

            var expandoObject = new ExpandoObject();
            // ExpandoObject'ler kendi içerisinde IDictionary yapısını implement ettiği için, direkt olarak Dictonary'ye cast edilebiliyor. 
            // Böylece Dictionary'nin Add metodu ile ExpandoObject içerisine dinamik olarak veri eklenebiliyor.
            // Eklenirken belirlenen key değeri ExpandoObject içerisindeki bir property yerine geçiyor, Ona verilecek value değeri ise
            // O propertynin değeri oluyor.
            var expandoDictionary = (IDictionary<string, object>)expandoObject;

            foreach (var keyValuePair in @this)
            {
                expandoDictionary.Add(keyValuePair);
            }
            return expandoObject;
        }

        /// <summary>
        /// <see cref="object"/> <paramref name="instance"/> nesnesini belirlenen <typeparamref name="T"/> nesnesine çevirmeye çalışan metot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T As<T>(this object instance)
        {
            // gönderilen değer null ise belirlenen generic type'ın default değeri döndürülür.
            if (instance == null) return default(T);

            // Eğer instance değeri bir T nesnesi ise direk cast işlemi gerçekleştiriliyor.
            if (instance is T)
                return (T)instance;

            try
            {
                // Eğer gönderilen değer bir T nesnesi değilse, Convert metotları aracılığıyla çevirme işlemi gerçekleştirlir.
                return (T)Convert.ChangeType(instance, typeof(T));
            }
            catch (InvalidCastException)
            {
                // Çevirme işlemi geçersiz çevirme denemesi dolayısıyla Exception fırlatırsa, T type'ının default değeri geri döndürülür.
                return default(T);
            }
        }

        /// <summary>
        /// <paramref name="obj"/> nesnesinin belirtilen isimdeki property'sinin değeri object olarak döndürür.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyNameToGetValueFrom"></param>
        /// <returns></returns>
        public static object GetValue(this object obj, string propertyNameToGetValueFrom)
        {
            return obj.GetType().GetProperty(propertyNameToGetValueFrom)?.GetValue(obj, null);
        }

        public static object Clone(this object obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return formatter.Deserialize(ms);
            }
        }

        public static void CopyValues(this object target, object source)
        {
            var t = target.GetType();

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }

        public static TP CopyValuesTo<TP>(this object source)
        {
            var tp = Activator.CreateInstance<TP>();

            var targetProperties = typeof(TP).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in targetProperties)
            {

                var value = source.GetValue(prop.Name);
                if (value != null)
                    prop.SetValue(tp, value, null);
            }
            return tp;
        }

    }
}

