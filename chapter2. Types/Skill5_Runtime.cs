using System;
using System.Reflection;
using System.Linq;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq.Expressions;
using System.Drawing;

namespace exam483.chapter2._Types
{
    class Skill5_Runtime
    {
        [Serializable]
        private class Test
        {

        }

        /* GetTypes = все типы, GetExportTypes = public типы
         *
         * ExpressionVisitor позволяет обойти дерево выражения и изменить его с помощью метода modify и переопределенного visit
         * NodeType  - операция выражения
         *
         * В типах есть propertyInfo для быстрого доступа к свойствам - canWrite, getMethod и тд
         * MethodInfo для выполнения методов
         *
         * Conditional - атрибут выполнения кода на основе define
         */

        public void Do()
        {
            // AttributesAndTypes();
            // CreateCodeDom();
            // Expressions();
        }

        private void Expressions()
        {
            // Прямое создание через лямбу
            Expression<Func<double, double>> square = x => x * x;
            square.Compile();

            // Создание через параметры
            ParameterExpression numPar = System.Linq.Expressions.Expression.Parameter(typeof(double), "num");
            BinaryExpression binaryExpression = System.Linq.Expressions.Expression.Multiply(numPar, numPar);

            Expression<Func<double, double>> square1 = Expression.Lambda<Func<double, double>>(binaryExpression, numPar);
            var doSquare = square1.Compile();
            Console.WriteLine("Квадрат 2 - " + doSquare(2));
        }

        private static void CreateCodeDom()
        {
            //Создаем корень компиляции
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            //Создаем пространство имен и импортируем System
            CodeNamespace nameSpace = new CodeNamespace("DynamicNameSpace");
            nameSpace.Imports.Add(new CodeNamespaceImport("System"));

            // Добавляем в дерево пространство имен
            compileUnit.Namespaces.Add(nameSpace);

            // Создаем тип 
            CodeTypeDeclaration type = new CodeTypeDeclaration("DynamicType")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };
            // Добавление поля типы string
            var nameField = new CodeMemberField("String", "name")
            {
                Attributes = MemberAttributes.Private
            };
            type.Members.Add(nameField);

            // Добавляем тип в пространство имен
            nameSpace.Types.Add(type);

            //Теперь нужен CodeDomProvider
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            var sw = new StringWriter();
            CodeGeneratorOptions opt = new CodeGeneratorOptions();
            provider.GenerateCodeFromCompileUnit(compileUnit, sw, opt);
            sw.Close();

            Console.WriteLine(sw.ToString());
        }

        private static void AttributesAndTypes()
        {
            if (Attribute.IsDefined(typeof(Test), typeof(SerializableAttribute)))
            {
                var attr = Attribute.GetCustomAttribute(typeof(Test), typeof(SerializableAttribute)) as SerializableAttribute;

                var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                            where !type.IsAbstract
                            select type;
            }
        }
    }
}
