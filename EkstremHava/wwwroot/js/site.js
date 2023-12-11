<script>
    function playThunderSound() {
            var audio = document.getElementById("thunder-audio");
    audio.pause();
    audio.muted = true; // Başlangıçta sesi kapalı yap
        }

    window.addEventListener("load", function () {
        playThunderSound();

    var audio = document.getElementById("thunder-audio");
    var soundButton = document.getElementById("toggle-sound-button");
    var isSoundOn = false;

    soundButton.addEventListener("mouseenter", function () {
                if (!isSoundOn) {
        soundButton.title = "Sesi Aç";
                }

    else {
        soundButton.title = "Sesi Kapat";
                }
            });

    function toggleSound() {
                if (isSoundOn) {
        audio.pause();
    isSoundOn = false;
    soundButton.title = "Ses Kapalı";
                } else {
        audio.play();
    isSoundOn = true;
    soundButton.title = "Ses Açık";
                }
            }

    soundButton.addEventListener("click", toggleSound);
        });

</script>
