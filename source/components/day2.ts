import { Token, Block } from "../core/tokenizer";
import { AoCComponent } from "../core/defs";

/**
 * Advent of Code 2019
 * Solution for Day 2. Part one of the puzzle
 */

export enum IntCodeOperation {
    add = 1,
    multiply = 2,
    halt = 99,
    void = -1
}

export class IntCodeInput {
    tokens: Array<Token>;
    blocks: Array<Block>
}

export class IntCodeProgram {
    public currentBlock: number = 0;
    public blockLength: number = 0;

    public intCodeSymbols = {
        opCode: 0, // Indice
        valueA: 1, // Indice + 1 is the first value
        valueB: 2,// Indice + 2 is the second value
        position: 3// Indice + 3 is the register/pos to store at
    };

    run(input: IntCodeInput) {
        this.blockLength = input.blocks.length;
        if (!input || !input.blocks[0].tokens[0]) {
            throw 'Something unexpectd found in IntCode input!';
        }

        // TODO this should be done in preProcess
        input.tokens[1].integer = 12;
        input.blocks[0].tokens[1].integer = 12;

        input.tokens[2].integer = 2;
        input.blocks[0].tokens[2].integer = 2;


        while (this.currentBlock <= this.blockLength) {
            let value = 0;
            const block = input.blocks[this.currentBlock];
            if (block.tokens.length < block.size) { return; }

            const opcode = block.tokens[this.intCodeSymbols.opCode];
            const position = block.tokens[this.intCodeSymbols.position];

            switch (opcode.integer) {
                case IntCodeOperation.add: value = this.add(block); break;
                case IntCodeOperation.multiply: value = this.multiply(block); break;
                case IntCodeOperation.halt: break;
                default: break; // Void id - this is a error
            }

            this.modify(input, value, position.integer);
            this.step();
        }
    }


    add(block: Block) {
        const valueA = block.tokens[this.intCodeSymbols.valueA];
        const valueB = block.tokens[this.intCodeSymbols.valueB];
        return valueA.integer + valueB.integer;
    }

    multiply(block: Block) {
        const valueA = block.tokens[this.intCodeSymbols.valueA];
        const valueB = block.tokens[this.intCodeSymbols.valueB];
        return valueA.integer * valueB.integer;
    }

    /** A IntCode program need to modify itself. 
     * 
     * In order to do that we need to find both the token index and then the block index, and set the value there. 
     * This is a bit overkill for now perhaps 
     * */
    modify(input: IntCodeInput, value: number, position: number) {
        if (input && input.tokens[position]) {
            const foundToken = input.tokens[position];
            const block = input.blocks.find((block) => block.id === foundToken.blockId);
            if (block && block.tokens) {
                block.tokens.find((t) => t.index === position).integer = value;
            }
        }
    }

    step() {
        if (this.currentBlock < this.blockLength) {
            return this.currentBlock++;
        }

        return false;
    }
}

export class AoCSolutionDay2 extends AoCComponent {
    public part1: boolean = false;

    public solve(): number {
        const intCode = new IntCodeProgram().run({
            blocks: this.blocks, tokens: this.tokens
        });

        debugger;
        return 1;
    }
}