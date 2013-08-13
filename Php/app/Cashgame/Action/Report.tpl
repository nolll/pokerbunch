{extends file="app/Page.tpl"}
{block name='title'}Report Stack{/block}
{block name=full}
	<form method="post" action="{$model->reportUrl->url}">
		<fieldset>
			<div class="block gutter">
				<h2>Report</h2>
				{partial view='app\Site\Errors' model=$model->validationErrors}
				<div>
					stack size:
					<input type="number" name="stack" id="stack" value="{$model->reportAmount}" class="numberfield" pattern="[0-9]*" required min="0"/>
				</div>
				<div class="buttons">
					<button type="submit" class="action">Report Stack</button>
				</div>
			</div>
		</fieldset>
	</form>
{/block}