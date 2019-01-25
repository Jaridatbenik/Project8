Init
{
<DialogFile>:DialoogMetEenVraag
}

Script 
{
[START]:<Regel1>
<Regel1>:<Regel2>
<Regel2>:(Vraag1)
(Vraag1):<Regel4>,<Regel7>,<Regel10>
<Regel4>:<Regel5>
<Regel5>:<Regel6>
<Regel6>:[EXIT]

<Regel7>:(Vraag2)
(Vraag2):<Regel9>,<Regel14>
<Regel9>:[EXIT]

<Regel10>:<Regel11>
<Regel11>:<Regel12>
<Regel12>:<Regel13>
<Regel13>:[EXIT]

<Regel14>:[EXIT]
}