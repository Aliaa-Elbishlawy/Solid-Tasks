using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace D02App1;

public class CustomContainerProgram
{
    static void Main(string[] args)
    {
        #region use simple injector
        var container = new SimpleInjector.Container();
        // -- if i want to change lifestyle in console app should use this options
        //container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.ThreadScopedLifestyle();

        container.Register<ICreditCard, MasterCard>();
        container.Verify();
        //using (ThreadScopedLifestyle.BeginScope(container))
        //{
        var card1 = container.GetInstance<ICreditCard>();
        card1.Charge();
        }
        #endregion

        #region StructureMap
        var container = new Container();

        container.For<ICreditCard>().Use<MasterCard>();

        var Service = container.GetInstance<ICreditCard>();

        #endregion

        // Intialization for the concrete classes
        // var card1 = new MasterCard();
        // var card2 = new VisaCard();
        // Shopper client = new Shopper();
        // client.Checkout(card2);

        // 1. Create container instance
        //CustomContainer container = new CustomContainer();

        // 2. Registration for types
        //container.Register<ICreditCard, MasterCard>();
        //container.Register<Shopper, Shopper>();

        // 3. Resolution for types to return concrete types
        //var card1 = container.Resolve<ICreditCard>();
        //var client1 = container.Resolve<Shopper>();

        //client1.Checkout(card1);

        // // override for registration
        // // any resolution after this registration will return "MasterCard"
        // container.Register<ICreditCard, MasterCard>();
        // client1.Checkout(card1);

        ////if non-generic & Original method version not private
        //container.Register(typeof(ICreditCard), typeof(VisaCard));

        //// 3. resolution for types to return concrete types
        //var card2 = container.Resolve<ICreditCard>();
        //var client2 = container.Resolve<Shopper>();

        //client2.Checkout(card2);
    }
}