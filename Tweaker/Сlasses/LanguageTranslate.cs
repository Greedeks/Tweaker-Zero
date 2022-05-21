using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Tweaker.Сlasses
{
    internal sealed class LanguageTranslate
    {
        internal string GetLanguageWindows()
        {
            CultureInfo _cultureInfo = CultureInfo.CurrentCulture;
            string _languageSystem = Regex.Replace(_cultureInfo.ToString(), @"-.+$", "", RegexOptions.Multiline);
            if (_languageSystem.ToString() == "ru")
                return _languageSystem.ToString();
            else return "eng";
        }

        internal string DescriptionTranslate()
        {
            if (SettingsTweaker.Language == "ru")
                return "Наведите указатель мыши на любую функцию, чтобы получить ее описание";
            else return "Hover over any function to get its description";
        }

        internal string ConfidentialityTweaksHover(in string _translate)
        {
            Dictionary<string, string> TweaksHoverRu = new Dictionary<string, string>
            {
                ["Tweak1"] = "В дополнение к объявлениям, которые вы видите в меню «Пуск» Windows, вам также присваивается уникальный идентификатор для отслеживания каждого вашего шага в магазине Windows и в приложениях, предоставляя вам конкретные рекламные предложения.",
                ["Tweak2"] = "Если вы используете локальную учетную запись, а не Microsoft, то вам следует знать: Windows даже в этом случае всегда пытается синхронизировать ваши данные с серверами Microsoft. В частности, следующее: темы, пароли, настройки Windows, настройки браузера, настройки языковой панели, настройки упрощённого доступа, дополнительные настройки.",
                ["Tweak3"] = "Телеметрия Windows – это совокупность процедур, направленных на сбор и обработку информации о вас и вашем поведении за компьютером. Microsoft с каждой новой версией Windows собирает и изучает всё больше и больше информации о том, какие приложения вы используете, как часто их запускаете, чем больше интересуетесь в интернете и многое-многое другое.",
                ["Tweak4"] = "В «Планировщике заданий» есть целый ряд служб, которые после своей работы собирают данные о своей работе и отправляют их в Microsoft.",
                ["Tweak5"] = "Особое внимание Microsoft уделяет изучению программного обеспечения пользователей. Составляет, так сказать, собственный топ программ и приложений, что даёт возможность обучаться на вас в реализации новых продуктов. Как показывает практика, Microsoft не использует полученные сведения для улучшения программного обеспечения и не развивает платформы для написания программ, если они занимают даже лидирующие позиции. ",
                ["Tweak6"] = "Microsoft не достаточно списка ваших приложений, она изучает ещё и успешность вашей работы в этих приложениях: как часто вы пользуетесь теми или иными приложениями, сколько вы тратите времени на работу в том или ином приложении.",
                ["Tweak7"] = "После внедрения в Windows такой опции, как рукописный ввод, Microsoft однажды решила, что можно улучшать технологию рукописного ввода в своей операционной системе, обучаясь на тех, кто пользуется этой самой Windows. То есть, даже если вы не используете рукописный ввод, Microsoft по умолчанию всё равно узнаёт об этом.",
                ["Tweak8"] = "Инструменты отслеживания действий пользователя не знают предела. Windows имеет ещё один сервис – UAR, призванный выполнять пошаговое протоколирование ваших действий за компьютером, среди которых: снятие скриншотов, набор текста, запуск программ, доступ к веб-камере на заблокированном экране и многое другое.",
                ["Tweak9"] = "Microsoft Windows получает ваше местоположение и использует данную информацию в рекламных целях.",
                ["Tweak10"] = "Даже если вы не используете «Обратную связь», Microsoft всё равно регистрирует частоту ваших обращений в официальную поддержку.",
                ["Tweak11"] = "Microsoft Windows умеет распознавать голос специальным модулем распознавания речи и синтеза речи. Даже если вы не используете Cortana или «Специальные возможности» Windows, ваша операционная система по умолчанию всегда занимается проверкой обновления своего движка распознавания голоса.",
                ["Tweak12"] = "Windows изучает пользователя по разным причинам. Но для пользователя важно одно – этот модуль не приносит пользы и не делает ничего ради пользователя.",
                ["Tweak13"] = "В групповой политике есть служба, направленная на тестирование ещё не вышедших функций на определённых конфигурациях ПК.",
                ["Tweak14"] = "DiagTruck и dmwappushservice - служат они, конечно, не пользователю а компании Microsoft и собирают данные о действиях и бездействиях пользователя.",
                ["Tweak15"] = "А что будет, если сеть недоступна, а всё то добро, которое собирают шпионы, нужно отправить... Есть на этот случай бэкап данных. И бэкапить Windows решила в «Журнал событий Windows».",
                ["Tweak16"] = "Телеметрия NVIDIA – это совокупность процедур, направленных на сбор и обработку информации о вас и вашем поведении за компьютером. NVIDIA решили, что, если Microsoft сходит с рук такой наглый сбор всего и вся с компьютеров пользователя, то почему бы и им не заняться тем же.",
            };

            Dictionary<string, string> TweaksHoverEng = new Dictionary<string, string>
            {
                ["Tweak1"] = "In addition to the ads you see in the Windows Start menu, you are also assigned a unique identifier to track your every move in the Windows Store and in apps, providing you with specific promotional offers.",
                ["Tweak2"] = "If you use a local account, and not Microsoft, then you should know: Windows, even in this case, always tries to synchronize your data with Microsoft servers. Specifically the following: themes, passwords, Windows settings, browser settings, language bar settings, Simplified access settings, additional settings.",
                ["Tweak3"] = "Windows telemetry is a set of procedures aimed at collecting and processing information about you and your behavior at the computer. With each new version of Windows, Microsoft collects and studies more and more information about what applications you use, how often you launch them, the more you are interested in the Internet, and much, much more.",
                ["Tweak4"] = "In the «Task Scheduler» there are a number of services that, after their work, collect data about their work and send it to Microsoft.",
                ["Tweak5"] = "Microsoft pays special attention to the study of user software. It makes, so to speak, its own top programs and applications, which makes it possible to learn from you in the implementation of new products. As practice shows, Microsoft does not use the information received to improve software and does not develop platforms for writing programs, even if they occupy leading positions.",
                ["Tweak6"] = "Microsoft does not have enough of a list of your applications, it also studies the success of your work in these applications: how often do you use these or other applications, how much time do you spend working in this or that application.",
                ["Tweak7"] = "After introducing such an option as handwriting into Windows, Microsoft once decided that it was possible to improve the handwriting technology in its operating system by learning from those who use this very Windows. That is, even if you don't use handwriting, Microsoft will still know about it by default.",
                ["Tweak8"] = "User activity tracking tools know no limit. Windows has another service – UAR, designed to perform step-by-step logging of your actions at the computer, including: taking screenshots, typing, running programs, accessing a webcam on a locked screen, and much more",
                ["Tweak9"] = "Microsoft Windows receives your location and uses this information for advertising purposes.",
                ["Tweak10"] = "Even if you don't use Feedback, Microsoft still registers the frequency of your calls to official support.",
                ["Tweak11"] = "Microsoft Windows is able to recognize voice with a special speech recognition and speech synthesis module. Even if you don't use Cortana or Windows' Accessibility Features, your default operating system is always checking for updates to its voice recognition engine.",
                ["Tweak12"] = "Windows studies the user for various reasons. But one thing is important for the user – this module does not benefit and does not do anything for the user.",
                ["Tweak13"] = "In group policy there is a service aimed at testing functions that have not yet been released on certain PC configurations.",
                ["Tweak14"] = "DiagTruck and dmwappushservice - they serve, of course, not the user, but Microsoft and collect data about the actions and inactions of the user.",
                ["Tweak15"] = "And what happens if the network is unavailable, and all the good that the spies collect needs to be sent... There is a backup of data for this case. And I decided to backup Windows to the «Windows Event Log».",
                ["Tweak16"] = "NVIDIA telemetry is a set of procedures aimed at collecting and processing information about you and your behavior at the computer. NVIDIA decided that if Microsoft gets away with such a brazen collection of everything and everything from the user's computers, then why don't they do the same.",
            };

            if (SettingsTweaker.Language == "ru")
                return TweaksHoverRu[_translate];
            else return TweaksHoverEng[_translate];
        }
    }
}
