Init
{
<DialogFile>:Dialog5
}

Script 
{
[START]:<Regel1>

<Regel1>:<Regel2>
<Regel2>:(Vraag1)

(Vraag1):<Vraag1Antwoord1>,<Vraag1Antwoord2>,<Vraag1Antwoord3>

<Vraag1Antwoord1>:(Vraag2)
<Vraag1Antwoord2>:(Vraag2)
<Vraag1Antwoord3>:(Vraag2)

(Vraag2):<Vraag2Antwoord1>,<Vraag2Antwoord2>,<Vraag2Antwoord3>

<Vraag2Antwoord1>:(Vraag3)
<Vraag2Antwoord2>:(Vraag3)
<Vraag2Antwoord3>:(Vraag3)

(Vraag3):<Vraag3Antwoord1>,<Vraag3Antwoord2>,<Vraag3Antwoord3>

<Vraag3Antwoord1>:<Regel3>
<Vraag3Antwoord2>:<Regel3>
<Vraag3Antwoord3>:<Regel3>

<Regel3>:[EXIT]
}