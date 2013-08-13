{extends file="app/Page.tpl"}
{block name='title'}End Game{/block}
{block name=full}
	<form method="post">
		<fieldset>
			<div class="block gutter">
				<h2>End Game</h2>
				<div>
					All done?
				</div>
				<div class="buttons">
					<button type="submit" class="action">Yes</button>
				</div>
			</div>
		</fieldset>
	</form>
{/block}