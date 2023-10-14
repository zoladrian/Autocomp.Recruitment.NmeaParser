# Autocomp.Recruitment.NmeaParser

<h1>Nmea Parser</h1>

<h2>Opis projektu</h2>
<p>Jest to projekt Parser NMEA, który służy do wyświetlania informacji zawartych w wiadomościach NMEA.</p>

<h2>Język i konwencje</h2>
<p>Komentarze w kodzie są w języku polskim, zgodnie z konwencją stosowaną w projekcie, który miałem za zadanie wykorzystać. Interfejs użytkownika (GUI) jest natomiast w języku angielskim.</p>

<h2>Zewnętrzne biblioteki</h2>
<ul>
    <li><strong>Prism</strong>: Struktura kodu, regionManager, wstrzykiwanie zależności, BindableBase.</li>
    <li><strong>FluentValidation</strong>: Walidacja zakresu danych.</li>
    <li><strong>xUnit</strong>: Testy jednostkowe.</li>
    <li><strong>Moq</strong>: Mockowanie testów jednostkowych.</li>
    <li><strong>MaterialDesign</strong>: Wygląd GUI.</li>
</ul>

<h2>Parsowanie</h2>
<p>Oba parsery korzystają z klas implementujących interfejs <code>IFieldParser</code>, gdzie parsowane są pojedyncze pola. Zdecydowałem się na takie rozwiązanie, aby nie łamać pierwszej zasady SOLID. Dodatkowo, do obsługi błędów i sprawdzania zakresu danych, użyłem biblioteki FluentValidation (np. czy stopnie są z zakresu 0-359). Przeparsowane wiadomości są przekazywane do widoku w <code>ParserViewModel</code> za pośrednictwem nadpisanej metody <code>ToString()</code> w modelach.</p>

<h2>Komunikacja między ViewModel a parserami</h2>
<p>Miałem problem z zastosowaniem wzorca strategii, ponieważ klasy parserów implementują ten sam interfejs generyczny, a klasy modeli nie mają wspólnych cech, które pozwoliłyby na utworzenie wspólnej abstrakcji. Wybrałem więc strategię opartą na refleksji. Myślałem również o innych sposobach rozszerzenia parsera, aby umożliwić interpretację wiadomości z różnymi nagłówkami. Wybrana "hybryda" wydaje mi się być najrozsądniejszą opcją, ale chętnie wysłucham innych pomysłów.</p>
