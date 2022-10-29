using FakerLib;
using TestClasses;
namespace Tests
{
    public class Tests
    {


        [Test]
        public void Simple_Test()
        {
            Faker faker = new Faker();

            InHuman human = faker.Create<InHuman>();

            Assert.Multiple(() =>
            {
                Assert.That(human, Is.Not.EqualTo(null));
                Assert.That(human.Name, Is.Not.EqualTo(null));
                Assert.That(human.Description, Is.Not.EqualTo(null));
            });
        }

        [Test]
        public void Cycle_Test()
        {
            Faker faker = new Faker();

            Human human = faker.Create<Human>();

            Assert.Multiple(() =>
            {
                Assert.That(human, Is.Not.EqualTo(null));
                Assert.That(human.Name, Is.Not.EqualTo(null));
                Assert.That(human.MyFamily, Is.Not.EqualTo(null));
                Assert.That(human.MyFamily.Father, Is.EqualTo(human));

            });
        }

        [Test]
        public void Array_Test()
        {
            Faker faker = new Faker();

            int[] ints = faker.Create<int[]>();

            Assert.Multiple(() =>
            {
                Assert.That(ints, Is.Not.EqualTo(null));
                Assert.That(ints.Length, Is.InRange(1,20));
            });
        }

        [Test]
        public void Array_Multidimensional_Test()
        {
            Faker faker = new Faker();

            int[,,] ints = faker.Create<int[,,]>();

            Assert.Multiple(() =>
            {
                Assert.That(ints, Is.Not.EqualTo(null));
                Assert.That(ints.Length, Is.InRange(1, 20*20*20));
                Assert.That(ints.Rank, Is.EqualTo(3));
            });
        }

        [Test]
        public void Config_Test()
        {
            FakerConfig config = new FakerConfig();
            config.Add<InHuman, string, NameGenerator>(inHuman => inHuman.Name);
            Faker faker = new Faker(config);


            InHuman human = faker.Create<InHuman>();

            Assert.Multiple(() =>
            {
                Assert.That(human, Is.Not.EqualTo(null));
                Assert.That(human.Name, Is.EqualTo("igor"));
                Assert.That(human.Description, Is.Not.EqualTo(null));
            });
        }

        [Test]
        public void Impossible_Test()
        {
            FakerConfig config = new FakerConfig();
            Faker faker = new Faker();


            Type type = faker.Create<Type>();

            Assert.Multiple(() =>
            {
                Assert.That(type, Is.EqualTo(null));
                
            });
        }
    }
}