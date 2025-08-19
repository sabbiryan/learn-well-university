using LearnWellUniversity.Application.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Extensions
{
    public static class PermissionHelper
    {
        public static List<string> GetAllPermissions()
        {
            var permissions = new List<string>();
            
            CollectPermissions(typeof(PermissionCodes), permissions);
            
            return permissions;
        }

        private static void CollectPermissions(Type type, List<string> list)
        {
            var constants = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

            foreach (var constant in constants)
            {
                list.Add(constant.GetValue(null)!.ToString()!);
            }



            var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            foreach (var nested in nestedTypes)
            {
                CollectPermissions(nested, list);
            }
        }
    }

}
