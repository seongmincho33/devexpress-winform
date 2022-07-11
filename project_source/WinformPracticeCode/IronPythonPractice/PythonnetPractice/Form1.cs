using Python.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PythonnetPractice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Start();
        }

        private void Start()
        {
            //Where python으로 나온 파이썬 설치경로를 설정
            var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\Users\SMJO\anaconda3\");
            //환경 변수 설정
            AddEnvPath(PYTHON_HOME, Path.Combine(PYTHON_HOME, @"Library\bin"));
            //python 홈 설정
            PythonEngine.PythonHome = PYTHON_HOME;
            //모듈 패키지 패스 설정
            PythonEngine.PythonPath = string.Join(Path.PathSeparator.ToString(), new string[]
            {
                PythonEngine.PythonPath,         
                //pip하면 설치되는 패키지 폴더.
                Path.Combine(PYTHON_HOME, @"Lib\site-packages"),    
                //개인 패키지 폴더
                @"C:\Users\SMJO\Desktop\SMJO_CODE\휴지통"
            });
            // Python 엔진 초기화
            PythonEngine.Initialize();
            // Global Interpreter Lock 을 취득
            using (Py.GIL())
            {
                // string 식으로 python식을 작성, 실행
                PythonEngine.RunSimpleString(@"
import sys;
print('hello world');
print(sys.version);
");
                //개인 패키지 폴더의 example/test.py를 읽어드린다.
                dynamic test = Py.Import("test");

                // example/test.py의 Calculator클래스를 선언
                dynamic f = test.Calculator(1, 2);
                //Calculator의 add함수를 호출
                this.richTextBox1.Text = f.add();
            }

            //파이썬 환경을 종료한다.
            PythonEngine.Shutdown();
        }

        // 환경설정 Path를 설정하는 함수이다. 실제 Path가 바뀌는 건 아니고 프로그램 세션 안에서만 path를 변경해서 사용한다.
        public static void AddEnvPath(params string[] paths)
        {
            // PC에 설정되어 있는 환경 변수를 가져온다.
            var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
            // 중복 환경 변수가 없으면 list에 넣는다.
            envPaths.InsertRange(0, paths.Where(x => x.Length > 0 && !envPaths.Contains(x)).ToArray());
            // 환경 변수를 다시 설정한다.
            Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
        }
    }
}
