# Need for Swim
- Need for Swim este un singleplayer endless în care un astronaut blocat pe fundul oceanului pe o planetă necunoscută este atacat de ființele extraterestre băștinașe. Deoarece armele nu funcționează sub apă, astronautul se poate baza doar pe sistemul de propulsie al propriului costum pentru a sări Mario-style și a spera că forța sa de aterizare va fi suficientă pentru a-și strivi adversarii. Scopul jocului este de a obține un scor cât mai mare

# Implementare
- Jocul a fost creat folosind Unity (version 2020.3.19f1) și CgFX.
- Pentru realizarea [astronautului](https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298) și a [inamiciilor](https://assetstore.unity.com/packages/3d/characters/creatures/meshtint-free-polygonal-metalon-151383) am folosit assets.

# Features

## AI - path finding
- Am implementat un AI de pathfinding pe inamic, astfel extraterestul va face target-lock pe caracterul nostru și ne va urmări în orice direcție am merge. Dacă suntem în raza sa de atac, atunci inamicul ne va ataca.
 
![Path Finding Image](https://drive.google.com/uc?export=view&id=115D3keHGRB9uDc1IaFcpLwm8UNq3PatL)


## Physical animation
Există animații pentru:
- deplasarea astronautului
- deplasarea extraterestrului (inamicul)
- saltul astronautului (atac)
- atacul extraterestrului
- înfrângerea/moartea inamicului

![Physical animation Image](https://drive.google.com/uc?export=view&id=1-5HZgTcZ2r8gCLrfvcOLKuep0sUYTCAm)

## Graphics
- Am implementat două shadere, pentru teren și pentru lumina albastră care simulează lumina din ocean. Algele de pe teren se generează automat în timp ce camera se apropie de ele.

![Graphics Image](https://drive.google.com/uc?export=view&id=19ToYJtJLC_6nRsTMdQ5-HllRyYgWRTja)

# Alte detalii tehnice 
## WaveSpawner
- Începem cu un număr prestabilit de mostrii, iar atunci cand toți sunt morți, incepe un nou wave, unde numărul este numărul de
monștri precedent din wave + 2

## Enemy Attack
- Inamicii au viteză variabilă și toți urmăresc jucătorul pana cand ajung suficient de aproape pentru a-l ataca

## Enemy Controller
- Inamicii detectează dacă un player este pe cale să pice pe aceștia (se află în aer deasupra lor). Dacă da, aceștia încearcă să fugă
de player (direcție random). Dacă nu, aceștia încearcă să fugă spre player pentru a-l lovi.
- De fiecare dată când un inamic moare, scorul player-ului crește cu 1.

## Player
- Se poate mișca folosind WASD și ataca apăsând Space
- Poate sări doar atunci când isGrounded() este true, tasta space este apăsată și nu ai cooldown pe săritură.
- Scorul este numărul de monștri doborâți de player.
- Combo este numărul de sărituri consecutive pe un inamici pe care player le-a realizat fără să atingă solul.

# Video
[LINK](https://drive.google.com/file/d/1-EHoVku81BgFz3d5-qwSafKO1Zn4FN_Z/preview)

# Echipă
Agha Mara 343, Chiricu Bianca 343, Dudau Claudia 343, Poinarita Diana 343, Strimbeanu Luana 343, Titrat Cristina 343
