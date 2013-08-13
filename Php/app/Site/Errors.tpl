{if $model != null}
    <div class="errors">
        {foreach $model as $error}
            <p class="validation-error">
                {$error}
            </p>
        {/foreach}
    </div>
{/if}