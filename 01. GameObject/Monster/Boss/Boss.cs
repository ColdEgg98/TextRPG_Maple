using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager;

namespace TextRPG_Maple._01._GameObject.Monster.Boss
{
    internal class Boss : Monster
    {
        public bool phase => Stat.Hp < Stat.MaxHp / 2;
        public bool triggerEvent { get; set; }
        public bool trigger
        {
            get => phase;
            set
            {
                if (value && triggerEvent)
                {
                    Phase2Event?.Invoke();
                    triggerEvent = false;
                }
            }
        }
        public bool isBoss { get; set; } = true;

        public event Action Phase2Event;
        public Boss(string name, int hp, int attack, int defense, int Exp, int Gold) : base(name, hp, attack, defense, Exp, Gold)
        {
            triggerEvent = true;
            Phase2Event += Boss2Phase;
        }
       

        public void DisplayBoss()
        {
            switch (Name)
            {
                case "마노":
                    Display_Mano();
                    break;
                case "혼테일":
                    Display_Horntail();
                    break;
                case "가디언 엔젤 슬라임":
                    Display_Guardian_Angel_Slime();
                    break;
            }
        }
        public void SoundBoss()
        {
            switch (Name)
            {
                case "마노":
                    SoundManager.Instance.PlaySound(SoundType.BGM, "GuardianAngelSlime", true);
                    break;
                case "혼테일":
                    SoundManager.Instance.PlaySound(SoundType.BGM, "HornTail", true);
                    break;
                case "가디언 엔젤 슬라임":
                    SoundManager.Instance.PlaySound(SoundType.BGM, "GuardianAngelSlime", true);
                    break;
            }
        }

        public void RenderBossHP() // 체력 한 칸(■)에 5%씩 표시
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("■");
            }

            float hpPer = (float)Stat.Hp / Stat.MaxHp * 20;

            int left = Console.GetCursorPosition().Left - 2;
            int top = Console.GetCursorPosition().Top;

            Console.SetCursorPosition(left, top);
            Console.SetCursorPosition(left, top);
            Console.Write("□");
            left -= 2;
        }

        public void Boss2Phase()
        {
            Console.WriteLine($"{this.Name}이 화난 것 같다!");
            this.Stat.Atk += (int)(this.Stat.Atk * 0.1f);
            this.Stat.Def += (int)(this.Stat.Def * 0.1f);
        }
        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다! (남은 HP: {Stat.Hp})");
        }

        #region BossSetUp
        public void BossSetUp()
        {
            switch (Name)
            {
                // 계층에 따라 HP, 공격력, 방어력, 경험치, 골드 증가
                case "혼테일":
                    Stat.Hp = 500;
                    Stat.MaxHp = Stat.Hp;
                    Stat.Atk = 70;
                    Stat.Def = 50;
                    Stat.Exp = 1000;
                    Stat.Gold = 2000;
                    break;
                case "가디언 엔젤 슬라임":
                    Stat.Hp = 1000;
                    Stat.MaxHp = Stat.Hp;
                    Stat.Atk = 100;
                    Stat.Def = 50;
                    Stat.Exp = 1000;
                    Stat.Gold = 3000;
                    break;
            }
        }
        #endregion

        #region AsciiArt
        private void Display_Mano()
        {
            InputManager.Instance.Write("\x1b[1;37m마노가 나타났다 !!\n\x1b[0m");
            Console.WriteLine("                        ;~!:~=                    \r\n                        !-*~-$                    \r\n                       ~ !...                     \r\n                          :*,                     \r\n                             ~  ~                 \r\n                             ; *~*                \r\n                             *! ;=                \r\n     =-:!$      ~:,,:=       @$  *                \r\n   #   ,~;!.   *   .-;*      @$                   \r\n  $. .-:;;;@  -,,~~~-;;*     ~$          @:       \r\n   ;:=!!!==~   $=!!!==!;     : --     =*:*=   -=- \r\n    $:~~:;!!: ;;:~~:;!!-,==.#;;:!!:=   .~     .-,!\r\n   ;:-,,-~;!!!;-,,,-~;!*=~~!;;*::;;:**;-      .!.!\r\n   *-.,,,~~;!*~,..,,~~;!=~~~~:::~~~:~~;!      ##! \r\n   !, .,,-~;!*~, .,,,~;!=~~~~~~~~~::~~~;!    $    \r\n  $~, .,,-~;!!-, .,,,~;!!*:~~~~:~~~~~~::*@:  @    \r\n  $~,..,,-~;!!~, .,,,~!!!=~:~~~~::~::~~~::!~$     \r\n *;,. ..,,,~~*,. ...,,~;!!!~~;:~:*~:;:~~~;!=*     \r\n  ;:~=$$!~~~;-~!!$$~~~:;*=::~;*:!!~:;;~;;!*;!     \r\n   !  @=        *@.    .=:;;:!$;*=~:!;:!;;!#      \r\n   , .;--::!*.!---::*!.:!;!*;!~*=;!:!$;*!;!*.     \r\n    .-   ..-;-    ..--!=!!!*;#:!:~=!!#$#!;!!,     \r\n    -      .*    ....--;=!$$!:.,:,:$!#;;$!!!@     \r\n   #.             .:~,-~!=::#~.,,,~;*;::$!!!@     \r\n   ,,             .-:-:-:=~-:..,,,-:#:--$*!!!     \r\n   -:       -    ..-:~;;:*,,~,,,,-~~*~--#=!!!     \r\n   #  ~~   -;.   .--:--;*;:,,,,,-~:;*!~~*=!!$     \r\n   # :~: .-;,, , ,~~*--;;*:,,,,-~;;::;!!!;=!@     \r\n   #  ~!.:*. .-:,,-**-,~;*,,,,--:;~~:;;;!;$!,     \r\n   $  ~;;!    !:~-:;:.-~;*-----~;:~:;:~;!;$*.     \r\n    . ~!-*       :;;-.-;;!----~:;~~;*::;*;$#      \r\n    @ ~! -       :;;..-;*!!~~~~:;::;*;;;*!=@      \r\n :;!: ~!         , ;.-;!;,~!!::;;:;;*;;!*!;-      \r\n ~ .~.-!           ;.-;!,,:;;::!;;;;!*!*!!!-      \r\n!...-.,!           ;--;!~.:!;;;!!;;;;!!!!!$       \r\n!.;,*.~!           ;--;;!$!;;;;!*;;;;;!!!!$       \r\n:...,-;;           *~,-;;;;;;;;!*;;;;;;;!!:       \r\n !---~;~           ;;~~~;;;;$!!!!*;;;;;!!=        \r\n ~;;;;!             :*;;;;*!$$$!!*!!!!!!*-        \r\n  -~~=               .~:::~-;!*$!!**!!!$*         \r\n     *               ..,,----:@#=;;!***!*         \r\n     =.             ....,,----~~!;;;;;;;!@        \r\n     .,.            ...,,,------~*=*;;;;;;!$      \r\n      ,-..       .....,,,,---------~$$$$$$$$*,    \r\n       ,::::~~:::::::::::;;;;;;;;;;;;;;;;;;;;;  ");


        }
        private void Display_Horntail()
        {
            InputManager.Instance.Write("\x1b[1;37m혼테일이 등장했다 !!\n\x1b[0m");
            Console.WriteLine("              .         .                         \r\n               $        =                         \r\n               !.      !,                         \r\n               !!      !,                         \r\n               ;=~     =~                         \r\n               ;=;.  , *!                         \r\n               ;;=~  :!:!                         \r\n               ,,*;.,*::,                         \r\n                *#!***$-                          \r\n       .!      !=$===$=:~  ; : ;                  \r\n      *:*:!*   .$=$==*$*.   !;;*;!                \r\n     *!===!$!-  :$*$$=#:  ~;!!==**:               \r\n    ~==!**$!*$=-:$#$$#$~~$$=:=**;*!~              \r\n    ;=$==$#**=##!$$**#=!##=!!#$*$$=;              \r\n    ;*@$$##*!$@@#$$==#=#@#$*##$#$#*.              \r\n   !:**##$*=!==@@#=$=#$#@$=!=*$#$**.~             \r\n   ~$==*$***!$#@##=##=##@#=!*=*$#=$*,             \r\n    :##=~.~==$@@$$$##$##@@$==,,;$$$,              \r\n    -,::     =##$$$##$###$*     :~.               \r\n        ,**. -=$$#$##$##$=  .!=.                  \r\n       -$@    **$#=##=#$**    #*~                 \r\n      $===!   *$=#$####$=:   ;*=$*                \r\n    ,**!=*=.~$=$*#$=$$#=$$$~.===;*$.              \r\n    ==$$;:=@##$!!=$==#*!!##@#=:;$##$              \r\n   $#@$#**$$#$$;::;$$;::!=$$$#**#$@#;             \r\n  ~#@@$$$:=*$$$!::;**;::*$$#!**;$$@@#~            \r\n ,#@@#$*==!*!$$****=****$$$*$;;:#=@@#$            \r\n $$@@=$!*::=*$###$####$###=;;:..$$$@@#;           \r\n.*$@#:$! ;:*~##=$;;**;!=$#$,*:- :#=#@;:           \r\n~.$@$;#,  *,*##$;;;!:;;;$##*,~  -#*#@=.           \r\n.-##,,$#$=;;$##$***$=!!!!=## :=$$=-=##.,          \r\n $@#,*$###@##=::~~~==~::;;$##@###=$:@@~           \r\n.,=# =$#$####=*;;;;!~;;;;!$####=#$$ #@-           \r\n -@# ;##*#####=;;;;=*;;;;==##@==##  #@:           \r\n ::# ;###*###*!::*!#$;:*;=$###=###  $.*           \r\n   *  !##=###=;;;;!#;;;;;!$##@$##*  !,.           \r\n   ,   $#####$*!!!!$*;!!!*###@##!   .             \r\n       -##@###$==$$$$$*==####@###~                \r\n       ,######$=***=****=######$@@$-.             \r\n       -==##$##$#####$#$###$#$==####=*:           \r\n   ;!!!!=###$=###########$=$##=*!;=!;#$$*!!       \r\n  .=$=$$$###!!==$######$==$####$$==$=*$$$#$$$$*!! \r\n    ,~.~,.~,  --~~~~~~~~-~~~~~~~~~~~~--------~~~- ");
        }

        private void Display_Guardian_Angel_Slime()
        {
            Console.WriteLine("\x1b[1;37m가디언 엔젤 슬라임이 등장했다 !!\n\x1b[0m");
            Console.WriteLine("\r\n\r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                          ,                    ,,                                                   \r\n                         . -                    .                                                   \r\n                         , ..                 , .,-,,,.                                             \r\n                         -                   ..  .,,,..~.                                           \r\n                         -  ~                .  ,:,. -~,-,                                          \r\n                             ,              ,  .,      ,--,                                         \r\n                        ~ ~  ,             -.   . :      ~,,                                        \r\n                        ,... ..           .,.  -.-,       --                                        \r\n                        ,, , ..           .,  ,., -       ,-:                                       \r\n                       .-     .           :.   .  -       .,-.                                      \r\n                       ....   ~           ,.   . .,.       ,.-                                      \r\n                       ~  ..   .        ,.  . ..  .,       .,,                                      \r\n                        ,     .        .   , .   ,,         ~-,                                     \r\n                        -~.   .     . ,..  .,  ..--.         -~                    .,.              \r\n                         . . ,...... ,.,,,  ~ .   ..         ,,-                 .-   .-.           \r\n                        -.   ~.,,.-:~,.,, ,.,.   -:,,        .-,.               .   .. .-           \r\n                        .,,. ~-,:,.,. .,,.--, . -..~--        .-~.             .,  .,,.             \r\n                        ,~,  ,~. .    .,,,,,~   -~ ....        .-~.                ....  ~          \r\n                        ..-. ., ..    .....,,..,.,-.~..         .-,,.          .   ...   -          \r\n                        ...,, ,:   .. ....,,.,~~~,~-...           ~, -.        ..,...    ~          \r\n                        .....~..-,. ,-,,-~,-~~~~-  ,....           ,,,.-,...,~~-,,,,,..  -          \r\n                        ....   ...,~~,...,,,,--,.    ,...            ..-~~~~~:-..,,,,,. ,.          \r\n                     .....,        ,........,-,,.    ......                     , .,,,,..           \r\n                    ......            ........,,.      ,...                      ..,--.,            \r\n                   ....-             ..........,,        ,.                        ...              \r\n                   ...,              ......,...,,.         ,                                        \r\n                   ...    ,:;;,     ..... ;!;;,.,,.         ,                                       \r\n         .. .     ...    -=~~*=.  .......!=:~**.,,..         .       .                              \r\n        .  ., .  ..,.,  .!;. :*=-.......:*;. :*:.,,.          , . ...,..,                           \r\n        ..   . -. ..,.  ,-    -=........:-    ,*..,.  ..       , -.    .-.                          \r\n        .,. .   .,,,,.. ,!-  ,;* ...... -!-  ,;*...,,....      ..    .,,.                           \r\n         . .    ,- ..,,,.!*-,!=-. .....,.**-.!=-.,,,......     .     .-.                            \r\n           ,, ..,.....  ..=!!=:-,,==~  ~..=!;=~..  ........   -    ..- .                            \r\n          -   ..,. ...   ..-~, .;:,,-;~:. .-~,...  ............   ..  .,                            \r\n          ,.   .,. ..,,.,......   ...... .......,,,,.........,.  ,   ..,                            \r\n           -.   .  ............   ..............................,,....,                             \r\n            .-. .  ...................... ............  ...... ....-,.                              \r\n             ~  .  ................       .................,,,.... .                                \r\n             ....  ..........                 ...........,,--.,.,,,,                                \r\n              .,. ..............         ..    .........,,---. ..-.                                 \r\n                .. ,,,...........            ........,,,----,.                                      \r\n                .  ,-,,,.......................,,,,,,,,-----,   ..                                  \r\n                 - .---,,................,,,,,,,,,,,,,-----,.                                       \r\n                  . ,---,,,,,,,,....,,,,,,,,,,,,,,,,------,.    ,                                   \r\n                  ., .---,,,,,,,,,,,,,,,,,,,,,,,,,-------,     .                                    \r\n                   ,.  ,--,,,,,,,,,,,,,,,,,,,,,,--------.     ~                                     \r\n                    ,,  .,---,,,,,,,,,,,,,,,,,,-------..     ,.                                     \r\n                    .,.    ,---,,,,,,,,,,,,,,-------,.      ,.                                      \r\n                      .-     .,--,,,,,,,,,,,------,.      .,                                        \r\n                        ..-    .,,,....,,,,,,,,,..      ,,.                                         \r\n                         ...~       .  .             .~ ..                                          \r\n                              .-. ..,..      .... .-..                                              \r\n                               ........    .......  .                                               \r\n                                   .......                                                          \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n");
        }
        #endregion

        #region Creator
        public Boss() : base() { }
        public Boss(Boss other) : base()
        {
            Stat = other.Stat.Clone();
            Name = other.Name;
        }
        public override GameObject Clone()
        {
            return new Boss(this);
        }
        #endregion
    }
}