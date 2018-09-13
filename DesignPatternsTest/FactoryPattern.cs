using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    class FactoryPattern
    {
    }
    /// <summary>
    /// 汽车抽象类
    /// </summary>
    public abstract class Car
    {
        // 开始行驶
        public abstract void Go();
    }
    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Factory
    {
        // 工厂方法
        public abstract Car CreateCar();
    }
    /// <summary>
    /// 奔驰车
    /// </summary>
    public class BenChiCar : Car
    {
        /// <summary>
        /// 重写抽象类中的方法
        /// </summary>
        public override void Go()
        {
            Console.WriteLine("奔驰车开始行驶了！");
        }
    }

    /// <summary>
    /// 奔驰车的工厂类
    /// </summary>
    public class BenChiCarFactory : Factory
    {
        /// <summary>
        /// 负责生产奔驰车
        /// </summary>
        /// <returns></returns>
        public override Car CreateCar()
        {
            return new BenChiCar();
        }
    }

    /// <summary>
    /// 客户端调用
    /// </summary>
    class Client
    {
        static void Main(string[] args)
        {

            // 如果客户又生产一辆奔驰车
            // 再另外初始化一个奔驰车的工厂
            Factory benChiCarFactory = new BenChiCarFactory();

            // 利用奔驰车的工厂生产奔驰车
            Car benChi = benChiCarFactory.CreateCar();
            benChi.Go();

            Console.Read();
        }
    }
}
