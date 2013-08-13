<nav class="heading-nav year-nav" data-require="heading-nav"><span>{$model->selected}</span><ul>
		{foreach $model->yearModels as $yearModel}
			<li><a href="{$yearModel->link->url}">{$yearModel->text}</a></li>
		{/foreach}
	</ul></nav>