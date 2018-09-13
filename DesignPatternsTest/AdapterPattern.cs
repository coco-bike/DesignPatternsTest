using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    public class AdapterPattern1
    {
        ///<summary>
        ///家里只有两个孔的插座，也懒得买插线板了，还要花钱，但是我的手机是一个有3个小柱子的插头，明显直接搞不定，那就适配吧
        ///</summary>
        class Client
        {
            static void Main(string[] args)
            {
                //好了，现在就可以给手机充电了
                TwoHoleTarget homeTwoHole = new ThreeToTwoAdapter();
                homeTwoHole.Request();
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 我家只有2个孔的插座，也就是适配器模式中的目标(Target)角色，这里可以写成抽象类或者接口
        /// </summary>
        public class TwoHoleTarget
        {
            // 客户端需要的方法
            public virtual void Request()
            {
                Console.WriteLine("两孔的充电器可以使用");
            }
        }

        /// <summary>
        /// 手机充电器是有3个柱子的插头，源角色——需要适配的类（Adaptee）
        /// </summary>
        public class ThreeHoleAdaptee
        {
            public void SpecificRequest()
            {
                Console.WriteLine("我是3个孔的插头也可以使用了");
            }
        }

        /// <summary>
        /// 适配器类，TwoHole这个对象写成接口或者抽象类更好，面向接口编程嘛
        /// </summary>
        public class ThreeToTwoAdapter : TwoHoleTarget
        {
            // 引用两个孔插头的实例,从而将客户端与TwoHole联系起来
            private ThreeHoleAdaptee threeHoleAdaptee = new ThreeHoleAdaptee();
            //这里可以继续增加适配的对象。。

            /// <summary>
            /// 实现2个孔插头接口方法
            /// </summary>
            public override void Request()
            {
                //可以做具体的转换工作
                threeHoleAdaptee.SpecificRequest();
                //可以做具体的转换工作
            }
        }
    }
    public class AdapterPattern2
    {

        /// <summary>
        /// 这里手机充电器为例，我们的家的插座是两相电的，但是手机的插座接头是三相电的
        /// </summary>
        class Client
        {
            static void Main(string[] args)
            {
                //好了，现在可以充电了
                ITwoHoleTarget change = new ThreeToTwoAdapter();
                change.Request();
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 我家只有2个孔的插座，也就是适配器模式中的目标角色（Target），这里只能是接口，也是类适配器的限制
        /// </summary>
        public interface ITwoHoleTarget
        {
            void Request();
        }

        /// <summary>
        /// 3个孔的插头，源角色——需要适配的类（Adaptee）
        /// </summary>
        public abstract class ThreeHoleAdaptee
        {
            public void SpecificRequest()
            {
                Console.WriteLine("我是三个孔的插头");
            }
        }

        /// <summary>
        /// 适配器类，接口要放在类的后面，在此无法适配更多的对象，这是类适配器的不足
        /// </summary>
        public class ThreeToTwoAdapter : ThreeHoleAdaptee, ITwoHoleTarget
        {
            /// <summary>
            /// 实现2个孔插头接口方法
            /// </summary>
            public void Request()
            {
                // 调用3个孔插头方法
                this.SpecificRequest();
            }
        }
    }
}
