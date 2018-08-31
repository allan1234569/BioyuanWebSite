function SliderLock(selector,callback) {

	//获取滑块的div
	////初始化组件
	var $div = $(selector);
	$div.append($("<div id='sliderLock_huakuai'><div></div></div><p id='sliderLock_status'>请按动滑块，拖到最右边</p>"));

	var $status = $("#sliderLock_status");
	var $btn = $("#sliderLock_huakuai");


//是否可拖动状态
	this.active = true;
	this.complete = function(){
		$div.addClass("slider_div_succeed");
		this.active = false;
		$status.text("完成验证");
		//状态栏 对齐
		var status_position = $status.offset();
		status_position.left = $div.offset().left+($div.innerWidth()-$status.outerWidth())/2;
		$status.offset(status_position);
		//滑块推至顶端
		var offset = $btn.offset();
		offset.left = $div.offset().left + $div.width()-$btn.width()-5;
		$btn.offset(offset);
	}
	this.failed = function () {
		this.active = true;
		this.refresh();
	}
	this.isActive = function(){
		return this.active;
	}
	//回调
	this.readCallback = callback;
	this.callback = function(){
		this.complete();
		this.readCallback.call();
	};
	//刷新
	this.refresh =function(){
		$div.removeClass("slider_div_succeed");
		this.active = true;
	}
	

	//status 位置
	var position = $div.position();
	position.top +=($div.outerHeight()-$status.height())/2;
	position.left +=($div.innerWidth()-$status.width())/2+20;
	$status.offset(position);

	//绑定滑动事件
	this.init = function(){
		this.registerSlideBtn();
			//可移动返回
		$div.addClass("slider_div");
		this.moveRange = {
			left:$div.offset().left,
			right:$div.offset().left+$div.width()-$btn.width()
		}

	}
	this.registerSlideBtn = function () {
		$btn.lastLeft = undefined;
		$btn.sliderLock = this;
		
		this.mousedownfunc = function () {
			if(!$btn.sliderLock.isActive()) return ;
				if(1==window.event.which){
					var ex = window.event.x;
					$btn.lastLeft=ex;
					$btn.mousedown = true;
					$btn.active=true;
				}
				return false;	
		};
		this.mouseupfunc = function(){
			if(!$btn.sliderLock.isActive()) return ;
			$btn.lasetLeft = undefined;
			$btn.mouseup = false;
			$btn.active=false;
		};
//移动该函数
		this.movefunc = function(){
			if(!$btn.sliderLock.isActive()) return ;
			if(window.event.which != 1) return;
			var ex = window.event.x;
			//判断坐标是否在btn之内
			var rectangle = {
				left:$div.offset().left,
				top:$btn.offset().top,
				bottom:$btn.offset().top+$btn.height(),
				right:$div.offset().left+$div.width()
			}
			var isIn = window.event.pageX>=rectangle.left&&window.event.pageX<=rectangle.right&&window.event.pageY>=rectangle.top&&window.event.pageY<=rectangle.bottom;
			if(!isIn) $btn.active = false;
			if(isIn&&$btn.active){
					if($btn.lastLeft==ex) return ;
					var change = ex - $btn.lastLeft;
					var p = $btn.offset();
					p.left +=change;
					p.left = Math.max(p.left,$btn.sliderLock.moveRange.left);
					p.left = Math.min(p.left,$btn.sliderLock.moveRange.right);
					$btn.offset(p);
					$btn.lastLeft = ex;	
					//如果差不多 就回调
					if(p.left>$btn.sliderLock.moveRange.right-15){ $btn.sliderLock.callback.call($btn.sliderLock);}
			}	
			window.event.stopPropagation();
			return false;

		};

		$("#sliderLock").mousemove(this.movefunc);
		
		$btn.mousedown(this.mousedownfunc);
		$btn.mouseup(this.mouseupfunc);
	}
}