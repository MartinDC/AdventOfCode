import { AOCConfig } from "../aoc";

export const renderableTags: any = {
    container: 'div',
    small: 'small',
    header: 'h3',
    paragraph: 'p',
    line: 'hr',
    bold: 'b',
}

export declare type CalendarEventCallback = (rawInput: string, calendarId: string) => void;
export declare type ComponentEventCallback = (rawInput: string, componentId: string) => void;

export class Display {
    renderContext: HTMLElement = null;
    inputElement: HTMLInputElement = null;

    constructor(public config: AOCConfig) {

    }

    registerContext() {
        const body: HTMLElement = document.getElementsByTagName('body')[0];
        const gameView: HTMLElement = document.querySelector(`${this.config.display.container}`);
        if (!gameView) { return console.log('Display init() - Failed to find game view'); }
        this.queryInput();

        gameView.classList.add('flex-container', 'w-100', 'h-100');
        body.style.backgroundColor = this.config.display.background;
        this.renderContext = gameView;
    }

    /** 
     * 
     * Register system events, this is for handling input and passing it on to the actuall application
     *
     * @param onInputReceived - Callback to be called from consumer of this API
     * */

    registerSystemEvents(calendarSelectedFn: CalendarEventCallback, componentSelectedFn: ComponentEventCallback) {
        this.renderContext.addEventListener('click', (event: MouseEvent) => {
            const target = event.target as HTMLElement;
            if (!target || !target.dataset.calendarId && !target.dataset.componentId) { return console.log('AdventOfCode19 - Unkown event'); }

            this.clearDisplay();
            const rawInput = this.queryInput();
            if (target.dataset.calendarId && calendarSelectedFn) {
                calendarSelectedFn(rawInput, target.dataset.calendarId);
            }

            if (target.dataset.componentId && componentSelectedFn) {
                componentSelectedFn(rawInput, target.dataset.componentId);
            }
        });
    }

    /** 
     * 
     * Append a new tag to the DOM
     *
     * @param tag - DOM-element to be created
     * @param text - Text content of the requested tag
     * */

    renderTag(tag: string, text: string, context?: Element, clazz?: string) {
        const availableTags: Array<string> = Object.keys(renderableTags).map((atag: string) => renderableTags[atag]);
        if (!availableTags.find((t) => t === tag)) { return console.log('Display renderTag - Unable to render unknow tag'); }
        const requestedTag = document.createElement(tag, {});

        requestedTag.textContent = text;
        requestedTag.className = clazz;
        if (context) {
            return context.appendChild(requestedTag);
        }

        this.renderContext.appendChild(requestedTag);
    }

    createTag(tag: string, text: string) {
        const requestedTag = document.createElement(tag, {});
        requestedTag.textContent = text;
        return requestedTag;
    }

    /** 
    * 
    * Get current state of the input element
    *
    * */

    queryInput() {
        if (!this.inputElement) {
            const inputElement: HTMLInputElement = document.querySelector(`${this.config.display.input}`);
            if (!inputElement) { console.error('Could not find input element - This is a fatal error!'); }
            this.inputElement = inputElement;
        }
        return this.inputElement.value;
    }

    /** 
     * 
     * * Append a new tag to a already exsiting DOM-element.
     *
     * */

    insertAdjacentTag(element: HTMLElement, tag: string, text: string) {
        const availableTags = Object.keys(renderableTags).map((atag) => renderableTags[atag]);
        if (!availableTags.find((t) => t === tag)) { return console.log('Display renderTag - Unable to render unknow tag'); }
        const requestedTag = document.createElement(tag, {});

        requestedTag.textContent = text;
        element.insertAdjacentElement('afterend', requestedTag);
    }

    /**
     * This function is called before the main input callback.
     * Usefull for rendering input information and simular.
     */

    clearDisplay() { this.renderContext.innerHTML = null; }
    clearInput() { this.inputElement.value = null; }
}