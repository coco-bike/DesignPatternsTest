using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    class StragetyPattern
    {
        //环境角色---相当于Context类型
        public sealed class SalaryContext
        {
            private ISalaryStrategy _strategy;

            public SalaryContext(ISalaryStrategy strategy)
            {
                this._strategy = strategy;
            }

            public ISalaryStrategy ISalaryStrategy
            {
                get { return _strategy; }
                set { _strategy = value; }
            }

            public void GetSalary(double income)
            {
                _strategy.CalculateSalary(income);
            }
        }

        //抽象策略角色---相当于Strategy类型
        public interface ISalaryStrategy
        {
            //工资计算
            void CalculateSalary(double income);
        }

        //程序员的工资--相当于具体策略角色ConcreteStrategyA
        public sealed class ProgrammerSalary : ISalaryStrategy
        {
            public void CalculateSalary(double income)
            {
                Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(" + 8000 + ")+加班费+项目奖金（10%）");
            }
        }

        //普通员工的工资---相当于具体策略角色ConcreteStrategyB
        public sealed class NormalPeopleSalary : ISalaryStrategy
        {
            public void CalculateSalary(double income)
            {
                Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(3000)+加班费");
            }
        }

        //CEO的工资---相当于具体策略角色ConcreteStrategyC
        public sealed class CEOSalary : ISalaryStrategy
        {
            public void CalculateSalary(double income)
            {
                Console.WriteLine("我的工资是：基本工资(" + income + ")底薪(20000)+项目奖金(20%)+公司股票");
            }
        }


        public class Client
        {
            public static void Main(String[] args)
            {
                //普通员工的工资
                SalaryContext context = new SalaryContext(new NormalPeopleSalary());
                context.GetSalary(3000);

                //CEO的工资
                context.ISalaryStrategy = new CEOSalary();
                context.GetSalary(6000);

                Console.Read();
            }
        }
    }
}
