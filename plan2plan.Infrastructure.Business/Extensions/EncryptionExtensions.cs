﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Business.Extentions
{
    public static class EncryptionExtensions
    {
        const int encryptionKey = 629;

        public static string EncryptionIDtoString(this int id)
        {
            id = id * encryptionKey;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(id.ToString());
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string EncryptionID(int id)
        {
            id = id * encryptionKey;
            return id.ToString();
        }

        /// <summary>
        /// При дешифровки проверяем на пустое значение и парсим к инту, если что-то пошло не так выдаем -1
        /// в удачном случае выдаем спрятанное число ;)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DecryptionID(this string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var z = Convert.FromBase64String(id);
                id = Encoding.UTF8.GetString(z);

                int result = -1;
                if (int.TryParse(id, out result))
                {
                    result = result / encryptionKey;
                }
                return result;
            }
            else
            {
                return -1;
            }
        }

        public static int DecryptionID(int id)
        {
            return id / encryptionKey;
        }

    }
}
