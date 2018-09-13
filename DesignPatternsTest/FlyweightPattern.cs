using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    public class FlyweightPattern
    {
        /// <summary>
        /// 享元模式不是很难，但是有些状态需要单独处理，以下就是该模式的C#实现，有些辅助类，大家应该看得出吧，别混了。
        /// </summary>
        class Client
        {
            static void Main(string[] args)
            {
                //比如，我们现在需要10000个一般士兵，只需这样
                SoldierFactory factory = new SoldierFactory();
                AK47 ak47 = new AK47();
                for (int i = 0; i < 100; i++)
                {
                    Soldier soldier = null;
                    if (i <= 20)
                    {
                        soldier = factory.GetSoldier("士兵" + (i + 1), ak47, SoldierType.Normal);
                    }
                    else
                    {
                        soldier = factory.GetSoldier("士兵" + (i + 1), ak47, SoldierType.Water);
                    }
                    soldier.Fight();
                }
                //我们有这么多的士兵，但是使用的内存不是很多，因为我们缓存了。
                Console.Read();
            }
        }

        //这些是辅助类型
        public enum SoldierType
        {
            Normal,
            Water
        }

        //该类型就是抽象战士Soldier--该类型相当于抽象享元角色
        public abstract class Soldier
        {
            //通过构造函数初始化士兵的名称
            protected Soldier(string name)
            {
                this.Name = name;
            }

            //士兵的名字
            public string Name { get; private set; }

            //可以传入不同的武器就用不同的活力---该方法相当于抽象Flyweight的Operation方法
            public abstract void Fight();

            public Weapen WeapenInstance { get; set; }
        }

        //一般类型的战士，武器就是步枪---相当于具体的Flyweight角色
        public sealed class NormalSoldier : Soldier
        {
            //通过构造函数初始化士兵的名称
            public NormalSoldier(string name) : base(name) { }

            //执行享元的方法---就是Flyweight类型的Operation方法
            public override void Fight()
            {
                WeapenInstance.Fire("士兵：" + Name + " 在陆地执行击毙任务");
            }
        }

        //这是海军陆战队队员，武器精良----相当于具体的Flyweight角色
        public sealed class WaterSoldier : Soldier
        {
            //通过构造函数初始化士兵的名称
            public WaterSoldier(string name) : base(name) { }

            //执行享元的方法---就是Flyweight类型的Operation方法
            public override void Fight()
            {
                WeapenInstance.Fire("士兵：" + Name + " 在海中执行击毙任务");
            }
        }

        //此类型和享元没太大关系，可以算是享元对象的状态吧，需要从外部定义
        public abstract class Weapen
        {
            public abstract void Fire(string jobName);
        }

        //此类型和享元没太大关系，可以算是享元对象的状态吧，需要从外部定义
        public sealed class AK47 : Weapen
        {
            public override void Fire(string jobName)
            {
                Console.WriteLine(jobName);
            }
        }

        //该类型相当于是享元的工厂---相当于FlyweightFactory类型
        public sealed class SoldierFactory
        {
            private static IList<Soldier> soldiers;

            static SoldierFactory()
            {
                soldiers = new List<Soldier>();
            }

            Soldier mySoldier = null;
            //因为我这里有两种士兵，所以在这里可以增加另外一个参数，士兵类型，原模式里面没有，
            public Soldier GetSoldier(string name, Weapen weapen, SoldierType soldierType)
            {
                foreach (Soldier soldier in soldiers)
                {
                    if (string.Compare(soldier.Name, name, true) == 0)
                    {
                        mySoldier = soldier;
                        return mySoldier;
                    }
                }
                //我们这里就任务名称是唯一的
                if (soldierType == SoldierType.Normal)
                {
                    mySoldier = new NormalSoldier(name);
                }
                else
                {
                    mySoldier = new WaterSoldier(name);
                }
                mySoldier.WeapenInstance = weapen;

                soldiers.Add(mySoldier);
                return mySoldier;
            }
        }
    }
}
