using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    class VisitorPattern1
    {
        //抽象图形定义---相当于“抽象节点角色”Element
        public abstract class Shape
        {
            //画图形
            public abstract void Draw();
            //外界注入具体访问者
            public abstract void Accept(ShapeVisitor visitor);
        }

        //抽象访问者 Visitor
        public abstract class ShapeVisitor
        {
            public abstract void Visit(Rectangle shape);

            public abstract void Visit(Circle shape);

            public abstract void Visit(Line shape);

            //这里有一点要说：Visit方法的参数可以写成Shape吗？就是这样 Visit(Shape shape)，当然可以，但是ShapeVisitor子类Visit方法就需要判断当前的Shape是什么类型，是Rectangle类型，是Circle类型，或者是Line类型。
        }

        //具体访问者 ConcreteVisitor
        public sealed class CustomVisitor : ShapeVisitor
        {
            //针对Rectangle对象
            public override void Visit(Rectangle shape)
            {
                Console.WriteLine("针对Rectangle新的操作！");
            }
            //针对Circle对象
            public override void Visit(Circle shape)
            {
                Console.WriteLine("针对Circle新的操作！");
            }
            //针对Line对象
            public override void Visit(Line shape)
            {
                Console.WriteLine("针对Line新的操作！");
            }
        }

        //矩形----相当于“具体节点角色” ConcreteElement
        public sealed class Rectangle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("矩形我已经画好！");
            }

            public override void Accept(ShapeVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        //圆形---相当于“具体节点角色”ConcreteElement
        public sealed class Circle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("圆形我已经画好！");
            }

            public override void Accept(ShapeVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        //直线---相当于“具体节点角色” ConcreteElement
        public sealed class Line : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("直线我已经画好！");
            }

            public override void Accept(ShapeVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        //结构对象角色
        internal class AppStructure
        {
            private ShapeVisitor _visitor;

            public AppStructure(ShapeVisitor visitor)
            {
                this._visitor = visitor;
            }

            public void Process(Shape shape)
            {
                shape.Accept(_visitor);
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                //如果想执行新增加的操作
                ShapeVisitor visitor = new CustomVisitor();
                AppStructure app = new AppStructure(visitor);

                Shape shape = new Rectangle();
                shape.Draw();//执行自己的操作
                app.Process(shape);//执行新的操作


                shape = new Circle();
                shape.Draw();//执行自己的操作
                app.Process(shape);//执行新的操作


                shape = new Line();
                shape.Draw();//执行自己的操作
                app.Process(shape);//执行新的操作


                Console.ReadLine();
            }
        }
    }
    class VisitorPattern2
    {
        //抽象访问者角色 Visitor
        public abstract class Visitor
        {
            public abstract void PutTelevision(Television tv);

            public abstract void PutComputer(Computer comp);
        }

        //具体访问者角色 ConcreteVisitor
        public sealed class SizeVisitor : Visitor
        {
            public override void PutTelevision(Television tv)
            {
                Console.WriteLine("按商品大小{0}排放", tv.Size);
            }

            public override void PutComputer(Computer comp)
            {
                Console.WriteLine("按商品大小{0}排放", comp.Size);
            }
        }

        //具体访问者角色 ConcreteVisitor
        public sealed class StateVisitor : Visitor
        {
            public override void PutTelevision(Television tv)
            {
                Console.WriteLine("按商品新旧值{0}排放", tv.State);
            }

            public override void PutComputer(Computer comp)
            {
                Console.WriteLine("按商品新旧值{0}排放", comp.State);
            }
        }

        //抽象节点角色 Element
        public abstract class Goods
        {
            public abstract void Operate(Visitor visitor);

            private int nSize;
            public int Size
            {
                get { return nSize; }
                set { nSize = value; }
            }

            private int nState;
            public int State
            {
                get { return nState; }
                set { nState = value; }
            }
        }

        //具体节点角色 ConcreteElement
        public sealed class Television : Goods
        {
            public override void Operate(Visitor visitor)
            {
                visitor.PutTelevision(this);
            }
        }

        //具体节点角色 ConcreteElement
        public sealed class Computer : Goods
        {
            public override void Operate(Visitor visitor)
            {
                visitor.PutComputer(this);
            }
        }

        //结构对象角色
        public sealed class StoragePlatform
        {
            private IList<Goods> list = new List<Goods>();

            public void Attach(Goods element)
            {
                list.Add(element);
            }

            public void Detach(Goods element)
            {
                list.Remove(element);
            }

            public void Operate(Visitor visitor)
            {
                foreach (Goods g in list)
                {
                    g.Operate(visitor);
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                StoragePlatform platform = new StoragePlatform();
                platform.Attach(new Television());
                platform.Attach(new Computer());

                SizeVisitor sizeVisitor = new SizeVisitor();
                StateVisitor stateVisitor = new StateVisitor();

                platform.Operate(sizeVisitor);
                platform.Operate(stateVisitor);

                Console.Read();
            }
        }
    }
}
