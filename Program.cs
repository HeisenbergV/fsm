using System;
using System.Threading;

namespace fsm
{
    class Program
    {
        public enum pk
        {
            atk,
            follow,
            back,
            chase
        }

        public enum Event
        {
            a,
            b,
            c
        }
        static void Main(string[] args)
        {
            FsmBuilder<pk> fsm = new FsmBuilder<pk>();
            fsm.Add(pk.follow).OnEnter((t)=>{
                System.Console.WriteLine("follow....");
                fsm.ChangeState(pk.back);
            }).OnCondition((int)Event.a, (t)=>{
                System.Console.WriteLine("a change chase");
                fsm.ChangeState(pk.chase);
            }).OnUpdate((t)=>{
                System.Console.WriteLine(".update follow...");
            });

            fsm.Add(pk.chase).OnEnter((t)=>{
                System.Console.WriteLine("chase .....");
            }).OnLeave((t)=>{
                System.Console.WriteLine("chase leave");
            });

            fsm.Add(pk.back).OnEnter((t)=>{
                System.Console.WriteLine(   "enter back");
            }).OnUpdate((t)=>{
                System.Console.WriteLine("back ......");
            });

            fsm.Startup(pk.follow);

            fsm.ChangeState(pk.follow);
        }
    }
}
