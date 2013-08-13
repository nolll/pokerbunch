{extends file="app/Page.tpl"}
{block name='title'}Cash Out{/block}
{block name=full}
	<form method="post" action="{$model->cashoutUrl->url}">
		<fieldset>
			<div class="block gutter">
				<h2>Cash Out</h2>
				{partial view='app\Site\Errors' model=$model->validationErrors}
				<div>
					stack size:
					<input type="number" name="stack" id="stack" value="{$model->cashoutAmount}" class="numberfield" pattern="[0-9]*" required min="0"/>
				</div>
				<div class="buttons">
					<button type="submit" class="action">Cash Out</button>
				</div>
			</div>
		</fieldset>
	</form>
{/block}