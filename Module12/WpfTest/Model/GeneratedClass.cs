using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Model
{
    //https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/how-to-create-a-class-using-codedom

    public class GeneratedClass
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }

        public Dictionary<CodeMemberField, CodeMemberProperty> fields { get; set; }

        public GeneratedClass()
        {
            fields = new Dictionary<CodeMemberField, CodeMemberProperty>();
        }

        public void Generate(string _Filename)
        {
            CodeCompileUnit TargetUnit = new CodeCompileUnit();
            CodeNamespace cns = new CodeNamespace(this.NameSpace);
            CodeTypeDeclaration TargetClass = new CodeTypeDeclaration(this.ClassName);
            TargetClass.IsClass = true;
            TargetClass.TypeAttributes = TypeAttributes.Public;

            foreach (KeyValuePair<CodeMemberField, CodeMemberProperty> kvp in fields)
            {
                TargetClass.Members.Add(kvp.Key);
                TargetClass.Members.Add(kvp.Value);
            }

            // Finnaly

            cns.Types.Add(TargetClass);
            TargetUnit.Namespaces.Add(cns);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(_Filename))
            {
                provider.GenerateCodeFromCompileUnit(
                    TargetUnit, sourceWriter, options);
            }
        }

        public void AddField(string _Name, Type _Type)
        {           
            CodeMemberField newMemberField = new CodeMemberField();

            newMemberField.Attributes = MemberAttributes.Private;
            newMemberField.Name = _Name;
            newMemberField.Type = new CodeTypeReference(_Type);

            CodeMemberProperty newCodeMemberProperty = new CodeMemberProperty();

            newCodeMemberProperty.Attributes = MemberAttributes.Public;
            
            StringBuilder sb = new StringBuilder(_Name);
            sb[0] = Char.ToUpper(sb[0]);
            newCodeMemberProperty.Name = sb.ToString();

            newCodeMemberProperty.HasGet = true;
            newCodeMemberProperty.HasSet = true;

            newCodeMemberProperty.Type = new CodeTypeReference(_Type);

            newCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), _Name)));
            newCodeMemberProperty.SetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), _Name)));

            fields.Add(newMemberField, newCodeMemberProperty);
        }
    }
}
