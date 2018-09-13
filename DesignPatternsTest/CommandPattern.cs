using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    class CommandPattern
    {
        /// <summary>
        /// 俗话说：“好吃不如饺子，舒服不如倒着”。今天奶奶发话要吃他大孙子和孙媳妇包的饺子。今天还拿吃饺子这件事来说说命令模式的实现吧。
        /// </summary>
        class Client
        {
            static void Main(string[] args)
            {
                //奶奶想吃猪肉大葱馅的饺子
                PatrickLiuAndWife liuAndLai = new PatrickLiuAndWife();//命令接受者
                Command command = new MakeDumplingsCommand(liuAndLai);//命令
                PaPaInvoker papa = new PaPaInvoker(command); //命令请求者

                //奶奶发布命令
                papa.ExecuteCommand();


                Console.Read();
            }
        }

        //这个类型就是请求者角色--也就是我爸爸的角色，告诉奶奶要吃饺子
        public sealed class PaPaInvoker
        {
            //我爸爸从奶奶那里接受到的命令
            private Command _command;

            //爸爸开始接受具体的命令
            public PaPaInvoker(Command command)
            {
                this._command = command;
            }

            //爸爸给我们下达命令
            public void ExecuteCommand()
            {
                _command.MakeDumplings();
            }
        }

        //该类型就是抽象命令角色--Commmand，定义了命令的抽象接口，任务是包饺子
        public abstract class Command
        {
            //真正任务的接受者
            protected PatrickLiuAndWife _worker;

            protected Command(PatrickLiuAndWife worker)
            {
                _worker = worker;
            }

            //该方法就是抽象命令对象Command的Execute方法
            public abstract void MakeDumplings();
        }

        //该类型是具体命令角色--ConcreteCommand，这个命令完成制作“猪肉大葱馅”的饺子
        public sealed class MakeDumplingsCommand : Command
        {
            public MakeDumplingsCommand(PatrickLiuAndWife worker) : base(worker) { }

            //执行命令--包饺子
            public override void MakeDumplings()
            {
                //执行命令---包饺子
                _worker.Execute("今天包的是农家猪肉和农家大葱馅的饺子");
            }
        }

        //该类型是具体命令接受角色Receiver，具体包饺子的行为是我们夫妻俩来完成的
        public sealed class PatrickLiuAndWife
        {
            //这个方法相当于Receiver类型的Action方法
            public void Execute(string job)
            {
                Console.WriteLine(job);
            }
        }
    }
}
