{if $model->enableAnalytics}
    <script>
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-8453410-7']);
        _gaq.push(['_trackPageview']);

        (function() {ldelim}
            var ga = document.createElement('script'); ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        {rdelim})();
    </script>
{/if}