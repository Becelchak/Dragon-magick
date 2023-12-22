
using System.Collections.Generic;
using JetBrains.Annotations;
using Mono.Cecil;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        //Saves?
        public PlayerSkin skin = 0;
        public DragonType enemy = 0;
        public int gold = 0;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        public Dragon NowDragon;
        public Character NowCharacter;

        public Character[] characters = new Character[3];
        public Dragon[] dragons = new Dragon[3];
        public enum PlayerSkin
        {
            Wanderer = 0,
            Cliric = 1,
            Piromant = 2,
        }

        public enum DragonType
        {
            Vivern = 0,
            SwampDragon = 1,
            MountainDragon = 2,
        }

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

        }

        public void Load()
        {
            characters[0] ??= new Character("Странствующий маг Легран",
                Resources.Load<Sprite>("GameObjects/Player/Wanderer Magican/Idle"),
                Resources.Load<RuntimeAnimatorController>("Animation/Wandered mage/Player_wanderer"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Nature/Wandered_1"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Nature/Wandered_2"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Nature/Wandered_3"),
                new List<int>() { 1, 1, 1, 1, 1 },
                new List<string> { "Магическая стрела", "Сгусток антиматерии", "Звездный дождь" },
                true,
                "Странствуюзий маг, практикующий магию ветра и звезд. Подрабатывает шутом на ярмарках; скопление воздуха наполненное магией. Крайне быстрое, но слабое заклинание; сгусток преисполненный маной. Медленный, но куда более серьезный удар в отличии от магической стрелы;  звездопад цельных звезд, вызванный при помощи всей маны. Попадет в любого врага под открытым небом",
                new List<int>() { 20, 45, 50, 60, 50 });

            characters[1] ??= new Character("Монахиня Мавра",
                Resources.Load<Sprite>("GameObjects/Player/Cliric Mage/Idle"),
                Resources.Load<RuntimeAnimatorController>("Animation/Cliric mage/Player_cliric"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Holy/Holy_13"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Holy/Holy_8"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Holy/Holy_14"),
                new List<int>() { 1, 1, 1, 1, 1 },
                new List<string> { "Духи света", "Солнечный меч", "Гнев свыше" },
                false,
                "Одинокая монахиня, давшая обет защищать слабых и обездоленных. Несколько раз чуть не была сожжена за колдовство; пучок света, самостоятельно летящий в дракона. Из-за своей природы имеет непостоянный урон от слабого до среднего; огромный луч солнечного света. Достаточно серьезное заклинание; призыв к силам за пределами человеческого понимания. Молния бьет всегда во врага, нанося чудовищный урон",
                new List<int>() { 40, 60, 80, 75, 85, 250 });

            characters[2] ??= new Character("Пиромант Магмощуп",
                Resources.Load<Sprite>("GameObjects/Player/Fire vizard/Idle"),
                Resources.Load<RuntimeAnimatorController>("Animation/Fire mage/Player_fire"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Fire/Fire_11"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Fire/Fire_5"),
                Resources.Load<Sprite>("GUI/SpellIconsVolume_1_Free/Fire/Fire_3"),
                new List<int>() { 1, 1, 1, 1, 1 },
                new List<string> { "Огненный меч", "Шар пламени", "Всплеск магмы" },
                false,
                "Старый маг, родом из далекого края. Получил прозвище когда остановил извержение вулкана. Любит курить табак; Столп пламени, возникающий там куда укажет рука пироманта. Серьезное заклинание; Самое излюбленное заклинание магов. Быстрое, сильное, простое в запоминании, главное им попасть; личная придумка Магмощупа. Наделяет огненный шар таким количеством маны, что он разделяется на три куска, каждый из которых содержит магму. Крайне сильное заклинание",
                new List<int>() { 80, 120, 160, 95, 100, 800 });

            dragons[0] ??= new Dragon("Красная виверна",
                Resources.Load<Sprite>("GameObjects/Dragon/Pixel_dragon_tilemap"),
                100,
                1,
                0.5f,
                "Малая красная виверна или просто красная виверна. Драконид обитающий в степных районах или лесах. За счет малых размеров считается слабейшим из драконидов, однако крайне плодовит, отчего считается наиболее часто встречаемым видом драконидов",
                1,
                true,
                0);

            dragons[1] ??= new Dragon("Болотный иглобрюх",
                Resources.Load<Sprite>("GameObjects/Dragon/Green Dragon"),
                250,
                1.6f,
                0.7f,
                "Bид земноводных драконидов, обитающих У водоемов, зачастую болот, что и отразилось в названии. Стоит отметить, что плюется он не иглами, а сгустком огня. Заблуждение, что он плюется огнем связано с его открытием, когда единственный выживший исследователь очнулся с множественными щепками застрявшими у него в теле.",
                3,
                false,
                300);

            dragons[2] ??= new Dragon("Горный дракон",
                Resources.Load<Sprite>("GameObjects/Dragon/Azure Dragon"),
                400,
                2.1f,
                0.3f,
                "Вид драконидов впадающих в спячку в соответствии с сезонами года. Более узкое его название - \"Левиафан\", за счет его размеров, которые обусловлены наличием жировых прослоек и крупных мышц, помогающих дракониду выживать в суровых горных условиях. В виду этой особенности плюется крайне специфичным огнем, который содержит в себе его жир и желчь, в перемешку с содержимым желудка. Такой огонь горит синеватым пламенем и крайне тяжело тухнет",
                4,
                false,
                600);
        }
    }
}
