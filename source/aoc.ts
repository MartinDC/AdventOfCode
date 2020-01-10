import { AoCSolutionDay1Part1 } from "./components/day1/day1-1";
import { AoCSolutionDay1Part2 } from "./components/day1/day1-2";
import { Display, renderableTags } from "./core/display";
import { AoCComponent } from "./core/defs";
import { Tokenizer, TokenizerConfig, defaultConfig } from "./core/tokenizer";

export class AOCConfig {
    display: {
        background: string,
        container: string,
        input: string,
        text: string
    };
    calendarMaxDays: number;
}

const config: AOCConfig = {
    display: {
        background: 'whitesmoke',
        container: '.game-display',
        input: '.game-input',
        text: '#a2a2a2'
    },
    calendarMaxDays: 25
}

declare type RenderCallback = (context: Display, component: AoCComponent) => void;
declare type ActionCallback = (context: Display, state: AdventOfCode19, id: string) => void;

class RenderActionArea {
    constructor(public name: string, public render: RenderCallback, public action: ActionCallback) {

    }
}

const createRenderActionArea = (name: string, renderfn: RenderCallback, actionfn: ActionCallback) => {
    return new RenderActionArea(name, renderfn, actionfn);
}

const calendarRenderActionArea: RenderActionArea = createRenderActionArea('calendar',
    (display: Display) => {
        const documentFragment = document.createDocumentFragment();
        for (let i = 1; i < config.calendarMaxDays + 1; i++) {
            const container: HTMLElement = display.createTag(renderableTags.container, '');
            const containerRefId = `solution-day-${i}`;

            if (container) {
                container.classList.add(containerRefId);
                container.classList.add('text-center');
                container.classList.add(`flex-item`, `card`, `hoverable`);
                container.dataset.calendarId = `${i}`;

                display.renderTag(renderableTags.header, `Day${i}`, container);
            }
            documentFragment.appendChild(container);
        }
        display.renderContext.appendChild(documentFragment);
    },
    (context: Display, aoc: AdventOfCode19, calendarId: string) => {
        context.clearDisplay();debugger;
        const components = aoc.components.filter((comp: AoCComponent) => {
            return comp.day.includes(`${calendarId}`);
        });

        components.forEach((component) => componentRenderActionArea.render(context, component));
    }
);

const componentRenderActionArea: RenderActionArea = createRenderActionArea('component',
    (display: Display, component: AoCComponent) => {
        if (!component) { return; }

        const containerRefId = `solution-part-${component.part}`;
        const documentFragment = document.createDocumentFragment();
        const container = display.createTag(renderableTags.container, '');

        if (container) {
            container.classList.add(containerRefId);
            container.classList.add('text-center');
            container.classList.add(`flex-item`, `card`, `hoverable`);
            container.dataset.componentId = `${component.id.key}`;

            display.renderTag(renderableTags.header, `Part - ${component.part}`, container);
        }

        documentFragment.appendChild(container);
        display.renderContext.appendChild(documentFragment);
    },
    (context: Display, aoc: AdventOfCode19, componentId: string) => {
        const component = aoc.components.find((comp: AoCComponent) => {
            return comp.part === `${componentId}`;
        });

        const input = aoc.processInput(context.queryInput());
        if (component && component.part && component.day) {
            const { blocks, tokens } = input.value;
            component.init(input.rawInput, tokens, blocks);
            aoc.currentComponent = component;
        }

        // Solve and print, and copy to clipboard
        const calculatedSolution = aoc.currentComponent.solve();
        const calculatedSolutionText = calculatedSolution.toString();
        const clipboard = (<any>window.navigator).clipboard;
        if (clipboard && clipboard.readText) {
            clipboard.writeText(calculatedSolutionText).then((): void => {
                console.log(calculatedSolution);
            })
        }

        if (!calculatedSolution || isNaN(calculatedSolution)) {
            context.renderTag(renderableTags.header, `Failed to solve this puzzle :(`, null, 'w-100 flex-justify-center');
            context.renderTag(renderableTags.small, `(You should probably check your input. Is it correct?)`, null, 'w-100 flex-justify-center');
            return;
        }

        //Render complete
        context.renderTag(renderableTags.header, `Puzzle solution is:`, null, 'w-100 flex-justify-center');
        context.renderTag(renderableTags.small, `(Solution is also copied to your clipboard)`, null, 'w-100 flex-justify-center');
        context.renderTag(renderableTags.header, `${calculatedSolution}`, null, 'w-100 flex-justify-center');
    }
);

export class AdventOfCode19 {
    public display: Display = null;
    public renderContext: HTMLElement = null;
    public currentComponent: AoCComponent = null;

    public tokenizer: Tokenizer = new Tokenizer();
    public components: Array<AoCComponent> = new Array<AoCComponent>();

    bootstrapApplication() {
        this.display = new Display(config);
        this.display.registerContext();
        return this;
    }

    registerComponent(component: AoCComponent, config: TokenizerConfig, preProcess?: (input: string) => string, postProcess?: (input: string) => string) {
        this.tokenizer.onPreProcess(preProcess);
        this.tokenizer.onPostProcess(postProcess);
        this.tokenizer.setConfig(config);
        this.components.push(component);
    }

    handleSystemEvents() {
        calendarRenderActionArea.render(this.display, null);
        const onCalendarSelected = (rawInput: string, calendarId: string) => {

            calendarRenderActionArea.render(this.display, null);
            calendarRenderActionArea.action(this.display, this, calendarId);
        }

        const onComponentSelected = (rawInput: string, componentId: string) => {
            const input = this.processInput(rawInput);
            if (input.error || !input.value) { return console.log('AdventOfCode19 - Could not read input'); }

            componentRenderActionArea.action(this.display, this, componentId);
        }

        this.display.registerSystemEvents(onCalendarSelected, onComponentSelected);
    }

    processInput(rawInput: string) {
        if (!rawInput || rawInput.length === 0) {
            return { error: true, value: null, rawInput: rawInput };
        }

        const processedInputData = this.tokenizer.process(rawInput);
        return { error: false, value: processedInputData, rawInput: rawInput };
    }
};

const adventOfCode = new AdventOfCode19().bootstrapApplication();

adventOfCode.registerComponent(new AoCSolutionDay1Part1('1', '1'), defaultConfig);
adventOfCode.registerComponent(new AoCSolutionDay1Part2('1', '2'), defaultConfig);

// adventOfCode.registerComponent(new AoCSolutionDay2('Day2'), intCodeConfig);

// adventOfCode.registerComponent(new AoCSolutionDay1('Day3'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day4'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day5'), defaultConfig);

// adventOfCode.registerComponent(new AoCSolutionDay1('Day6'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day7'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day8'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day9'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day10'), defaultConfig);

// adventOfCode.registerComponent(new AoCSolutionDay1('Day11'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day12'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day13'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day14'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day15'), defaultConfig);

// adventOfCode.registerComponent(new AoCSolutionDay1('Day16'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day17'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day18'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day19'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day20'), defaultConfig);

// adventOfCode.registerComponent(new AoCSolutionDay1('Day21'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day22'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day23'), defaultConfig);
// adventOfCode.registerComponent(new AoCSolutionDay1('Day24'), defaultConfig);

adventOfCode.handleSystemEvents();

